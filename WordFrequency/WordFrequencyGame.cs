﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        public string GetResult(string inputStr)
        {
            if (Regex.Split(inputStr, @"\s+").Length == 1)
            {
                return inputStr + " 1";
            }
            else
            {
                //split the input string with 1 to n pieces of spaces
                string[] arr = Regex.Split(inputStr, @"\s+");

                var inputList = GetInputList(arr);

                //get the map for the next step of sizing the same word
                var map = GroupByInputValue(inputList);

                var noDuplicateInputList = NoDuplicateInputList(map);

                return string.Join("\n", FormatOutput(noDuplicateInputList).ToArray());
            }
        }

        private static List<Input> NoDuplicateInputList(Dictionary<string, List<Input>> map)
        {
            List<Input> noDuplicateInputList = new List<Input>();
            foreach (var entry in map)
            {
                Input input = new Input(entry.Key, entry.Value.Count);
                noDuplicateInputList.Add(input);
            }

            noDuplicateInputList.Sort((w1, w2) => w2.WordCount - w1.WordCount);
            return noDuplicateInputList;
        }

        private static List<string> FormatOutput(List<Input> noDuplicateInputList)
        {
            List<string> strList = new List<string>();

            //stringJoiner joiner = new stringJoiner("\n");
            foreach (Input w in noDuplicateInputList)
            {
                string s = w.Value + " " + w.WordCount;
                strList.Add(s);
            }

            return strList;
        }

        private static List<Input> GetInputList(string[] arr)
        {
            List<Input> inputList = new List<Input>();
            foreach (var s in arr)
            {
                Input input = new Input(s, 1);
                inputList.Add(input);
            }

            return inputList;
        }

        private Dictionary<string, List<Input>> GroupByInputValue(List<Input> inputList)
        {
            Dictionary<string, List<Input>> map = new Dictionary<string, List<Input>>();
            foreach (var input in inputList)
            {
                //       map.computeIfAbsent(input.getValue(), k -> new ArrayList<>()).add(input);
                if (!map.ContainsKey(input.Value))
                {
                    List<Input> arr = new List<Input>();
                    arr.Add(input);
                    map.Add(input.Value, arr);
                }
                else
                {
                    map[input.Value].Add(input);
                }
            }

            return map;
        }
    }
}
