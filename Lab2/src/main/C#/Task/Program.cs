using System;
using System.Text;

namespace TOALab2
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            char[] alphabet = new char[] { 'a', 'b','c', '.', '_' };
            char?[] partialTape = new char?[] { 'a', 'c', 'b', 'a', 'b', 'a', 'b', 'c' };
            int headPosition = 0;
            int realTapeLength = 1000;
            int state = 0;
            int animationSpeed = 1000;
            bool animationMode = true;
            TuringMachine turingMachine = new TuringMachine(alphabet, headPosition, partialTape, realTapeLength, state, animationSpeed, animationMode);
        }
    }

    class TuringMachine
    {
        public bool animationMode;

        public TuringMachine(char[] alphabet, int headPosition, char?[] partialTape, int realTapeLength, int state, int animationSpeed, bool animationMode)
        {
            this.animationMode = animationMode;
            Dictionary<char, Transition[]> matrix = TransitionMatrixInput();
            if (IsWrongMatrix(alphabet, matrix, partialTape))
            {
                Console.WriteLine("Wrong input.");
            }
            else
            {
                char?[] realTape = RealTapeInitialize(realTapeLength);
                Run(alphabet, headPosition, matrix, realTape, partialTape, state, animationSpeed);
            }
        }

        char?[] PartialTapeRigthSideExtend(char?[] partialTape)
        {
            char?[] newPartialTape = new char?[partialTape.Length + 1];
            for (int i = 0; i < partialTape.Length; i++)
            {
                newPartialTape[i] = partialTape[i];
            }
            newPartialTape[newPartialTape.Length - 1] = '_';
            return newPartialTape;
        }

        char?[] PartialTapeLeftSideExtend(char?[] partialTape, ref int headPositionShift)
        {
            char?[] newPartialTape = new char?[partialTape.Length + 1];
            newPartialTape[0] = '_';
            for (int i = 0; i < partialTape.Length; i++)
            {
                newPartialTape[i + 1] = partialTape[i];
            }
            headPositionShift++;
            return newPartialTape;
        }

        void OutPutTransitionMatrix(Dictionary<char, Transition[]> matrix, char[] alphabet, int state, char selectedRow)
        {
            var keys = matrix.Keys;
            Transition[] firstTransitionsRow = matrix[keys.First()];
            int step = 0;
            int markTimes = 0;
            Console.WriteLine(GetStringOfAlphabetElems(firstTransitionsRow));
            foreach (var key in keys)
            {
                Transition[] transitions = matrix[key];
                Console.Write($"{alphabet[step]}" + " | ");
                for (int k = 0; k < transitions.Length; k++)
                {
                    bool selected = CheckIfCurrentTransitionIsSelected(transitions[k], state, selectedRow, key);
                    Console.Write($"{ConvertOneTransitionToString(transitions[k], selected, ref markTimes)}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" | ");
                }
                markTimes = 0;
                Console.WriteLine();
                step++;
            }
        }

        bool CheckIfCurrentTransitionIsSelected(Transition transition, int state, char selectedRow, char currentRow)
        {
            return transition.newState == state && selectedRow == currentRow; 
        }

        string GetStringOfAlphabetElems(Transition[] transitions)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new String(' ', 5));
            for (int i = 0; i < transitions.Length; i++)
            {
                sb.Append($"s{i}" + new String(' ', 5));
            }
            return sb.ToString();
        }

        string ConvertOneTransitionToString(Transition transition, bool boolean, ref int markTimes)
        {
            if (boolean && markTimes == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                markTimes++;
            }
            else Console.ForegroundColor = ConsoleColor.White;
            StringBuilder sb = new StringBuilder();
            sb.Append($"{(char)transition.valueForReplacing}");
            sb.Append($"{(char)transition.move}" );
            sb.Append($"s{transition.newState}".Replace("s-1", "# "));
            return sb.ToString();
        }

        bool IsWrongMatrix(char[] alphabet, Dictionary<char, Transition[]> matrix, char?[] partialTape)
        {
            var keys = matrix.Keys;
            int numberOfStates = CountStates(matrix);
            foreach(char? c in partialTape)
            {
                if (!alphabet.Contains(Convert.ToChar(c))) return true;
            }
            foreach (var key in keys)
            {
                if (!alphabet.Contains(key)) return true;
                else
                {
                    Transition[] transitions = matrix[key];
                    foreach (Transition tr in transitions)
                    {
                        if (!alphabet.Contains(tr.valueForReplacing)) return true;
                        if (tr.newState > numberOfStates || tr.newState < -1) return true;
                    }
                }
            }
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (!matrix.ContainsKey(alphabet[i])) return true;
            }
            return false;
        }

        int CountStates(Dictionary<char, Transition[]> matrix)
        {
            var keys = matrix.Keys;
            int maxCount = 0;
            int count = 0;
            foreach (var key in keys)
            {
                Transition[] transitions = matrix[key];
                if(maxCount < (count = transitions.Count())) maxCount = count;
            }
            return maxCount;
        }

        char?[] RealTapeInitialize(int length)
        {
            char?[] realTape = new char?[length];
            for (int i = 0; i < length; i++)
            {
                realTape[i] = null;
            }
            return realTape;
        }

        void Run(char[] alphabet, int headPosition, Dictionary<char, Transition[]> matrix, 
            char?[] realTape, char?[] partialTape, int state, int animationSpeed)
        {
            int headPositionShift = 0;
            int iterationNumber = 0;
            Output('\0', state, partialTape, ref iterationNumber, headPosition, alphabet, matrix, animationSpeed, headPositionShift);
            while (state != -1)
            {
                char? currentValue = partialTape[headPosition + headPositionShift];
                Transition[] row = matrix[Convert.ToChar(currentValue)];
                char valueForReplacing = row[state].valueForReplacing;
                ValueReplace(valueForReplacing, ref partialTape, headPosition, headPositionShift);
                Movement(row[state].move, ref partialTape, ref headPosition, ref headPositionShift, iterationNumber);
                state = row[state].newState;
                RealTapeInput(partialTape, headPositionShift, ref realTape);
                
                Output(valueForReplacing, state, partialTape, ref iterationNumber, headPosition, alphabet, matrix, animationSpeed, headPositionShift);
            }
        }

        void ValueReplace(char valueForReplacing, ref char?[] partialTape, int headPosition, int headPositionShift)
        {
            partialTape[Math.Abs(-headPositionShift - headPosition)] = valueForReplacing;
        }

        void Movement(Move move, ref char?[] partialTape, ref int headPosition, ref int headPositionShift, int iterationNumber)
        {
            if (CheckIfNearTheRightEdge(partialTape, headPosition, headPositionShift))
            {
                if(move == Move.Right)
                {
                    partialTape = PartialTapeRigthSideExtend(partialTape);
                    headPosition++;
                }
                else headPosition--;
            }
            else if (CheckIfNearTheLeftEdge(partialTape, headPosition, headPositionShift, iterationNumber, move))
            {
                if (move == Move.Left)
                {
                    partialTape = PartialTapeLeftSideExtend(partialTape, ref headPositionShift);
                    headPosition--;
                }
                else headPosition++;
            }
            else
            {
                if (move == Move.Left) headPosition--;
                else headPosition++;
            }
        }

        bool CheckIfNearTheRightEdge(char?[] partialTape, int headPosition, int headPositionShift)
        {
            if (partialTape.Length - 1 - headPositionShift == headPosition) return true;
            else return false;
        }

        bool CheckIfNearTheLeftEdge(char?[] partialTape, int headPosition, int headPositionShift, int iterationNumber, Move move)
        {
            if (iterationNumber == 0)
            {
                if(move == Move.Left) return true;
            }
            else if (move == Move.Left)
            {
                if (headPosition == -headPositionShift) return true;
            }
            return false;
        }

        char?[] GetPartialTape(char?[] realTape, int headPositionShift)
        {
            char? next = ' ';
            int step = 0;
            int count = 0;

            while (next != null)
            {
                count++;
                step++;
                next = realTape[realTape.Length / 2 + step - headPositionShift];
            }
            step = 0;
            next = ' ';
            char?[] partialTape = new char?[count];
            while (next != null)
            {
                partialTape[step] = realTape[realTape.Length / 2 + step - headPositionShift];
                step++;
                next = realTape[realTape.Length / 2 + step - headPositionShift];
            }
            return partialTape;
        }

        bool IsOverflow(int realTapeLength, char?[] partialTape)
        {
            if (realTapeLength / 2 < partialTape.Length + 1)
            {
                return true;
            }
            return false;
        }

        char?[] RealTapeInput(char?[] partialTape, int headPositionShift, ref char?[] realTape)
        {
            int centerPos = realTape.Length / 2;
            for (int i = 0; i < partialTape.Length; i++)
            {
                realTape[centerPos + i - headPositionShift] = partialTape[i];
            }
            return realTape;
        }

        void Output(char valueForReplacing, int state, char?[] tape, ref int iterationNumber, int headPosition, 
            char[] alphabet, Dictionary<char, Transition[]> matrix, int animationSpeed, int headPositionShift)
        {
            if(animationMode) Console.Clear();

            if (iterationNumber > 0)
            {
                Console.WriteLine($"Iteration {iterationNumber} ({valueForReplacing} → s{state}).".Replace("s-1", "#"));
            }
            else
            {
                Console.WriteLine($"Given (state = s{state}).");
            }
            string stringfOfTape = string.Join("] [", tape);
            Console.WriteLine("Tape: [" + stringfOfTape + ']');
            Console.WriteLine(new String(' ', 7 + Math.Abs(-headPositionShift - headPosition) * 4) + "↑");
            Console.WriteLine(new String(' ', 5 + Math.Abs(-headPositionShift - headPosition) * 4) + "Head");
            Console.WriteLine(new String(' ', 7 + Math.Abs(-headPositionShift - headPosition) * 4) + $"{headPosition}");
            Console.WriteLine();
            OutPutTransitionMatrix(matrix, alphabet, state, valueForReplacing);
            Console.WriteLine();
            iterationNumber++;
            Thread.Sleep(animationSpeed);
        }

        Dictionary<char, Transition[]> TransitionMatrixInput()
        {
            Dictionary<char, Transition[]> matrix = new Dictionary<char, Transition[]>();
            /*matrix.Add('1', new Transition[] { new Transition('1', Move.Right, 0) , new Transition('1', Move.Right, 0) });
            matrix.Add('_', new Transition[] { new Transition(',', Move.Right, 1) , new Transition('_', Move.Left, 0) });
            matrix.Add(',', new Transition[] { new Transition('_', Move.Right, -1)});*/

            /*matrix.Add('0', new Transition[] { new Transition('1', Move.Left, 0) });
            matrix.Add('1', new Transition[] { new Transition('0', Move.Left, 0) });
            matrix.Add('_', new Transition[] { new Transition('_', Move.Left, -1) });*/


            /*matrix.Add('0', new Transition[] { new Transition('1', Move.Right, 1), new Transition('1', Move.Right, 0) });
            matrix.Add('1', new Transition[] { new Transition('0', Move.Right, 1), new Transition('0', Move.Right, 0) });
            matrix.Add('e', new Transition[] { new Transition('e', Move.Right, -1), new Transition('e', Move.Right, -1) });
            matrix.Add('o', new Transition[] { new Transition('o', Move.Right, -1), new Transition('o', Move.Right, -1) });
            matrix.Add('_', new Transition[] { new Transition('e', Move.Right, -1), new Transition('o', Move.Right, -1) });*/

            matrix.Add('a', new Transition[] {new Transition('a', Move.Right, 1), new Transition('a', Move.Right, 0), new Transition('.', Move.Right, 0) });
            matrix.Add('b', new Transition[] { new Transition('b', Move.Right, 0), new Transition('.', Move.Left, 2) });
            matrix.Add('c', new Transition[] { new Transition('c', Move.Right, 0), new Transition('c', Move.Right, 0) });
            matrix.Add('.', new Transition[] { new Transition('.', Move.Right, 0) });
            matrix.Add('_', new Transition[] { new Transition('_', Move.Right, -1), new Transition('_', Move.Right, -1), new Transition('_', Move.Right, -1) });

            /*matrix.Add('0', new Transition[] { new Transition('0', Move.Left, 1), new Transition('0', Move.Left, 2), new Transition('0', Move.Left, -1) });
            matrix.Add('1', new Transition[] { new Transition('1', Move.Left, 1), new Transition('1', Move.Left, 2), new Transition('1', Move.Left, -1) });*/

            /*matrix.Add('0', new Transition[] { new Transition('0', Move.Right, 0) });
            matrix.Add('1', new Transition[] { new Transition('1', Move.Right, 0) });
            matrix.Add('_', new Transition[] { new Transition('_', Move.Right, 0) });*/


            /*matrix.Add('0', new Transition[] { new Transition('0', Move.Left, 0), });
            matrix.Add('1', new Transition[] { new Transition('1', Move.Left, 0), });
            matrix.Add('_', new Transition[] { new Transition('_', Move.Left, 0), });*/

            /*matrix.Add('_', new Transition[] {
                new Transition('_', Move.Left, 1),
                new Transition('_', Move.Left, 2),
                new Transition('_', Move.Right, 3),
                new Transition('_', Move.Right, 4),
                new Transition('_', Move.Right, 5),
                new Transition('_', Move.Right, 6),
                new Transition('_', Move.Right, 7),
                new Transition('_', Move.Right, -1)
            });*/
            return matrix;
        }
    }

    public class Transition
    {
        public char valueForReplacing;
        public Move move;
        public int newState;

        public Transition(char valueForReplacing, Move move, int newState)
        {
            this.valueForReplacing = valueForReplacing;
            this.move = move;
            this.newState = newState;
        }
    }

    public enum Move
    {
        Right = '→',
        Left = '←'
    }
}