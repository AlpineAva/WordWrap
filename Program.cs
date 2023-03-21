namespace HelloWorld
{
    class Program
    {

        static List<string> testCases = new List<string>() {
            "",
            "1234567890123456789090000 00000000000000000",
            "a     a",
            "1234567800 asdf asdf asdf asdf asdf asdf asd asd asd asd",
        };

        static void Main(string[] args)
        {
            var l = new Library();
            foreach (var test in testCases)
            {
                Console.WriteLine(".............................");
                Print(l.WordWrapPreservingWholeWordsWITHHYPHENS(test, 7));
                //Console.WriteLine(".............................");
                //Console.WriteLine(l.WordWrapString(test, 7));
            }
        }

        static void Print(List<string> l)
        {
            l.ForEach(i => Console.WriteLine(i));
        }
    }

    public class Library
    {
        public List<string> WordWrapPreservingWholeWordsWITHHYPHENS(string input, int maxLineLength)
        {
            var result = new List<string>();
            List<string> words = input.Split(" ").ToList();

            string currentLine = "";

            // TODO if time allows, improve readability
            foreach (var word in words)
            {
                // start a new line if there would be overflow with the new word
                if (currentLine != "" && currentLine.Length + word.Length + 1 > maxLineLength)
                {
                    result.Add(currentLine); // store currnet line
                    currentLine = ""; // start a new line...
                }

                if (currentLine == "")
                {
                    if (word.Length > maxLineLength)
                    {
                        var wrap = this.WordWrap(word, maxLineLength - 1); // ADD HYPHENS

                        // TINY BUG IF LAST LINE IF 1 CHARACTER... CAN DETECT WITH IF STATEMENT AND HANDLE...
                        for (int i = 0; i < wrap.Count - 1; i++)
                        {
                            if (i == wrap.Count - 2 && wrap.Last().Length == 1)
                            {
                                wrap[i] += wrap[i + 1]; // don't hyphen if last line is 1 character
                                wrap = wrap.Take(wrap.Count - 1).ToList();
                            }
                            else
                            {
                                wrap[i] += "-";
                            }
                        }
                        result.AddRange(wrap.Take(wrap.Count - 1)); // Add all but last one to the result list
                        currentLine = wrap.Last(); // set currentline to the last value...
                    }
                    else
                    {
                        currentLine += word; // edge case TODO if word longer than maxLineLength...
                    }
                }
                else
                {
                    currentLine += " " + word;
                }
            }

            if (currentLine != "")
            {
                result.Add(currentLine);
            }

            return result;
        }

        // input "one-twotwoo", 7
        // "one"
        // "twoooo"
        // "oooo"
        //
        // "one two"
        // "ooooooo"
        public List<string> WordWrapPreservingWholeWords(string input, int maxLineLength)
        {
            var result = new List<string>();
            List<string> words = input.Split(" ").ToList();

            string currentLine = "";

            // TODO if time allows, improve readability
            foreach (var word in words)
            {
                // start a new line if there would be overflow with the new word
                if (currentLine != "" && currentLine.Length + word.Length + 1 > maxLineLength)
                {
                    result.Add(currentLine); // store currnet line
                    currentLine = ""; // start a new line...
                }

                if (currentLine == "")
                {
                    if (word.Length > maxLineLength)
                    {
                        var wrap = this.WordWrap(word, maxLineLength); // ADD hyphens
                        result.AddRange(wrap.Take(wrap.Count - 1)); // Add all but last one to the result list
                        currentLine = wrap.Last(); // set currentline to the last value...
                    }
                    else
                    {
                        currentLine += word; // edge case TODO if word longer than maxLineLength...
                    }
                }
                else
                {
                    currentLine += " " + word;
                }
            }

            if (currentLine != "")
            {
                result.Add(currentLine);
            }

            return result;
        }

        public List<string> WordWrap(string input, int maxLineLength)
        {
            var result = new List<string>();

            int currentIndex = 0;
            while (currentIndex < input.Length)
            {
                int lengthToGet = Math.Min(maxLineLength, input.Length - currentIndex);  // get the number of characters to grab for this line...
                result.Add(input.Substring(currentIndex, lengthToGet));
                currentIndex += maxLineLength; // go to the next character in the string...
            }

            return result;
        }

        public string WordWrapString(string input, int maxLineLength) => string.Join("\n", WordWrap(input, maxLineLength));
    }
}

