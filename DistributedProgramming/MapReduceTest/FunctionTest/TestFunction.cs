using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionTest
{
    public static class TestFunction
    {
        [FunctionName("TestFunction")]
        public static async Task<string[]> RunOrchestrator([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            string text = context.GetInput<string>();

            string onlyText = Regex.Replace(text, @"\.|;|:|,|[0-9]|'|_", string.Empty);
            MatchCollection words = Regex.Matches(onlyText, @"[\w]+");
            string[] wordArray = words.Cast<Match>().Select(x => x.Value).ToArray();

            List<string[]> outputs = wordArray.Select(x => new string[] { x }).ToList();
            while (outputs.Count > 1)
            {
                List<Task<string[]>> tasks = new List<Task<string[]>>();
                for (int i = 0; i < outputs.Count / 2 * 2; i += 2)
                    tasks.Add(context.CallActivityAsync<string[]>("MergeTwo", (outputs[i], outputs[i + 1])));
                string[][] result = await Task.WhenAll(tasks);
                List<string[]> newOutputs = new List<string[]>();
                newOutputs.AddRange(result);
                if (outputs.Count % 2 == 1)
                    newOutputs.Add(outputs[outputs.Count - 1]);
                outputs = newOutputs;
            }

            return outputs[0];
        }

        [FunctionName("MergeTwo")]
        public static string[] MergeTwo([ActivityTrigger] (string[] first, string[] second) array, ILogger log)
        {
            return MergeTwo(array.first, array.second);
        }

        static string[] MergeTwo(string[] array1, string[] array2)
        {
            List<string> result = new List<string>();
            int i = 0, j = 0;
            while (i < array1.Length && j < array2.Length)
            {
                if (string.CompareOrdinal(array1[i], array2[j]) > 0)
                    result.Add(array1[i++]);
                else
                    result.Add(array2[j++]);
            }
            if (i < array1.Length)
                result.AddRange(array1.Skip(i));
            if (j < array2.Length)
                result.AddRange(array2.Skip(j));
            return result.ToArray();
        }

        [FunctionName("HttpStartTest")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            string data = await req.Content.ReadAsStringAsync();
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("TestFunction", data);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}