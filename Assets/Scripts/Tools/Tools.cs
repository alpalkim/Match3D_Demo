using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static List<int> GenerateRandomUniqueNumbers(int count, int minNumber, int maxNumber)
    {
        List<int> numbers = new List<int>(count);

        if ((maxNumber - minNumber) <= count)
        {
            for (int i = 0; i < maxNumber; i++)
            {
                numbers.Add(i);
            }

            return numbers;
        }

        else
        {
            do
            {
                int number = Random.Range(minNumber, maxNumber);
                if (!numbers.Contains(number))
                {
                    numbers.Add(number);
                }
            } while (numbers.Count < count);
        }

        


        return numbers;
    }

    public static List<ToyPair> GetRandomToyPrefabs(List<ToyPair> toyPool, int count)
    {
        List<ToyPair> pairs = new List<ToyPair>(count);
        List<int> randomNumbers = GenerateRandomUniqueNumbers(count, 0, toyPool.Count);
        Debug.Log(randomNumbers.Count);
        for (int i = 0; i < randomNumbers.Count; i++)
        {
            pairs.Add(toyPool[randomNumbers[i]]);
        }



        return pairs;
    }


    public static List<ToyPair> GetRandomToys(List<ToyPair> toyPool, int toyCount)
    {
        List<ToyPair> toyPairs = new List<ToyPair>(toyCount);
        List<int> randomInt = new List<int>(toyCount);

        do
        {
            int i = Random.Range(0, toyPool.Count);
            if (toyPool[i].leftToyPiece && toyPool[i].leftToyPiece && !randomInt.Contains(i))
            {
                randomInt.Add(i);
            }

        } while (randomInt.Count <= toyCount);

        foreach (int index in randomInt)
        {
            toyPairs.Add(toyPool[index]);
        }
        

        return toyPairs;
    }


    public static Dictionary<int, ToyPair> GenerateRandomToyPool(Dictionary<int, ToyPair> toyPool, int count, int maxNumber)
    {
        if (maxNumber >= toyPool.Count) { return toyPool; }

        List<int> numbers = Tools.GenerateRandomUniqueNumbers(count, 0, maxNumber);

        Dictionary<int, ToyPair> randomPool = new Dictionary<int, ToyPair>();

        foreach (int number in numbers)
        {
            if (toyPool.TryGetValue(number, out ToyPair pair))
            {
                randomPool.Add(number, pair);
            }
        }

        return randomPool;
    }

}
