using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgrammingChallengesCode.DataStructures
{
    public class CryptKicker
    {
        private const char Star = '*';
        private const int WordMaxLength = 16;
        private const int NumLetters = 26;
        private Dictionary<int, List<string>> lengthIndexedDictionary; 
        private string[] _dictionaryWords;
        private string[] _encryptedWords;
        public CryptKicker(String[] dictionaryWords, string encryptedText)
        {
            if (dictionaryWords != null && dictionaryWords.Any()) this._dictionaryWords = dictionaryWords;
            this.lengthIndexedDictionary =  new Dictionary<int, List<string>>();
            foreach (string dictionaryWord in _dictionaryWords)
            {
                IndexDictionaryWord(dictionaryWord);
            }
            if (!string.IsNullOrWhiteSpace(encryptedText))
            {
                this._encryptedWords = encryptedText.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(this.Decrypt(this._encryptedWords));
            }
        }


        /// <summary>
        /// Given a dictionary word, index it appropriately based on its length.
        /// </summary>
        /// <param name="dictionaryWord">Dictionary word.</param>
        private void IndexDictionaryWord(string dictionaryWord)
        {
            int index = dictionaryWord.Length;
            List<string> indexedList;
            if (!lengthIndexedDictionary.TryGetValue(index, out indexedList))
            {
                indexedList = new List<string>();
            }
            indexedList.Add(dictionaryWord);
            lengthIndexedDictionary[index] = indexedList;
        }


        private string Decrypt(string[] encryptedWords)
        {
            char[] mappings = new char[NumLetters];
            for (int i = 0; i < mappings.Length; i++)
            {
                mappings[i] = Star;
            }
            mappings = MapEncryptedWordsToDictionaryWords(0, mappings, encryptedWords);
            return BuildDecryptedTextFromMappings(mappings, encryptedWords);
        }

        private string BuildDecryptedTextFromMappings(char[] mappings, string[] envryptedText)
        {
            StringBuilder builder =  new StringBuilder();
            foreach (string encryptedWord in envryptedText)
            {
                foreach (char encryptedChar in encryptedWord)
                {
                    if (mappings != null)
                    {
                        builder.Append(mappings[encryptedChar - 'a']);
                    }
                    else
                    {
                        builder.Append(" ");
                    }
                }
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="mappings"></param>
        /// <param name="encryptedWords"></param>
        /// <returns></returns>
        private char[] MapEncryptedWordsToDictionaryWords(int i, char[] mappings, string[] encryptedWords)
        {
            // all encrypted words have been successfully encrypted.
            if (i == encryptedWords.Length) return mappings;
            else
            {
                foreach (string dictionaryWord in lengthIndexedDictionary[encryptedWords[i].Length])
                {
                    char[] localMappings = new char[mappings.Length];
                    Array.Copy(mappings, localMappings, localMappings.Length);
                    if (MappingsPossible(encryptedWords[i], dictionaryWord, localMappings))
                    {
                        localMappings = MapEncryptedWordsToDictionaryWords(i + 1, localMappings, encryptedWords);
                        if (localMappings != null)
                        {
                            return localMappings;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// MappingsPossible("abc","xyz") returns true whereas MappingsPossible("iif","abc") returns false
        /// because i will be mapped to both a and b.
        /// </summary>
        /// <param name="encryptedWord"></param>
        /// <param name="plainTextWord"></param>
        /// <param name="localMappings"></param>
        /// <returns></returns>
        private bool MappingsPossible(string encryptedWord, string plainTextWord, char[] localMappings)
        {
            if (encryptedWord.Length != plainTextWord.Length) return false;
            char[] encryptedChars = encryptedWord.ToCharArray();
            char[] plainTextChars = plainTextWord.ToCharArray();

            for (int i = 0; i < encryptedChars.Length; i++)
            {
                char encryptedChar = encryptedChars[i];
                char plainTextChar = plainTextChars[i];

                // gives the index of the encrypted char.
                int index = encryptedChar - 'a';
                
                // inidcates a conflict. encrypted character has already been mapped to some other character.
                if (localMappings[index] != Star && localMappings[index] != plainTextChar)
                {
                    return false;
                }
                else
                {
                    localMappings[index] = plainTextChar;
                }
            }

            // ensures that mapping[the -> all] is false i.e two differents char don't map to the same char.
            return Injective(localMappings);
        }

        /// <summary>
        /// Method to make sure that no two distinct characters in the encrypted 
        /// text map to the same plain text character.
        /// </summary>
        /// <param name="localMappings">Mappings.</param>
        /// <returns>true/false.</returns>
        private bool Injective(char[] localMappings)
        {
            bool[] injectiveMappings = new bool[NumLetters];
            foreach (char mappedChar in localMappings)
            {
                if (mappedChar != Star)
                {
                    int mappedCharIndex = mappedChar - 'a';
                    if (injectiveMappings[mappedCharIndex]) return false;
                    injectiveMappings[mappedCharIndex] = true;
                }
            }
            return true;
        }
    }

    
}
