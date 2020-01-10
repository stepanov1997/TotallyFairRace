#!/bin/bash

yarn jar /usr/hdp/current/hadoop-mapreduce-client/hadoop-streaming.jar \
    -files wasbs:///Mapper.exe,wasbs:///Reducer.exe \
    -mapper Mapper.exe \
    -reducer Reducer.exe \
    -input $1 \
    -output /example/sort1

numLines=2
i=1

while [ $numLines -gt 1 ]
do

yarn jar /usr/hdp/current/hadoop-mapreduce-client/hadoop-streaming.jar \
    -files wasbs:///Mapper.exe,wasbs:///Reducer.exe \
    -mapper Mapper.exe \
    -reducer Reducer.exe \
    -input /example/sort$i/part-00000 \
    -output /example/sort$(($i+1))

i=$(($i+1))

hdfs dfs -text /example/sort$i/part-00000 > output.txt

numLines=$(cat output.txt | wc -l)

done