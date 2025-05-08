using System.Diagnostics;
using System.Text;

namespace AlgebraicExpressionDemo
{
    public partial class MainPage : ContentPage
    {
        public Dictionary<char, int> SuperScriptMap = new Dictionary<char, int>
        {
        { '\u00B9', 1 }, // ¹
        { '\u00B2', 2 }, // ²
        { '\u00B3', 3 }, // ³
        { '\u2074', 4 }, // ⁴
        { '\u2075', 5 }, // ⁵
        { '\u2076', 6 }, // ⁶
        { '\u2077', 7 }, // ⁷
        { '\u2078', 8 }, // ⁸
        { '\u2079', 9 }  // ⁹
        };

        public Dictionary<char, string> superScriptDict = new Dictionary<char, string>
        {
        {'0', "\u2070"}, // ⁰
        {'1', "\u00B9"}, // ¹
        {'2', "\u00B2"}, // ²
        {'3', "\u00B3"}, // ³
        {'4', "\u2074"}, // ⁴
        {'5', "\u2075"}, // ⁵
        {'6', "\u2076"}, // ⁶
        {'7', "\u2077"}, // ⁷
        {'8', "\u2078"}, // ⁸
        {'9', "\u2079"}  // ⁹
        };

        public char[] digits = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public char[] operators = { '*', '/', '+', '-' };
        public char[] alphabet = {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z'
    };
        public Dictionary<char, int> valuesLetter = new Dictionary<char, int>();
        public List<string> expressionlist = new List<string>();
        private Class1 class1;
        public MainPage()
        {
            InitializeComponent();
        }

        public void DeleateAll(Object seter, EventArgs e) 
        {
            rs.Text = "";   
        }

        public void Start(Object seter, EventArgs e)
        {
            rs.Text = "";
            if (!string.IsNullOrEmpty(output.Text) && !output.Text.Contains(" "))
            {
                string expression = output.Text;
                rs.Text += $"                       1.multiplicacion\n";
                string resultFinal = SeparateExpression(expression);
                rs.Text += $"RESULTADO: {resultFinal}\n";
                class1 = new Class1(rs);
                class1.Add(resultFinal);
            }
            else 
            {
                rs.Text = "No hay nada que resolver";
            }
        }

        public string SeparateExpression(string expression)
        {
            StringBuilder builder = new StringBuilder();
            expressionlist = new List<string>();

            try
            {
                for (int i = 0; i <= expression.Length - 1; i++)
                {
                    if (operators.Contains(expression[i]))
                    {
                        if (expression[i] == '*' || expression[i] == '+' || expression[i] == '-' || expression[i] == '/')
                        {

                            expressionlist.Add(builder.ToString());
                            builder.Clear();
                            builder.Append(expression[i]);
                            builder.Append(expression[i + 1]);
                            i += 1;
                        }
                        else
                        {
                            expressionlist.Add(builder.ToString());
                            builder.Clear();
                            builder.Append(expression[i]);
                        }
                    }
                    else
                    {
                        builder.Append(expression[i]);
                    }
                }

                expressionlist.Add(builder.ToString());
                Debug.WriteLine($"lista que quiero ver: en multiplicate {string.Join(",", expressionlist)}");
                return ConformationMultiplication(expressionlist);
            }
            catch (Exception e) 
            {
                rs.Text = "Sintaxis erronea";
            }

            return null;
        }

        public string ConformationMultiplication(List<string> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                string currentItem = list[i];
                string previousItem = list[i - 1];

                if (currentItem.Contains("*"))
                {
                    if (previousItem[0] == '-' || digits.Contains(previousItem[0]))
                    {
                        rs.Text += $"1.numeros a multiplicar: {previousItem}{currentItem}\n";
                    }
                    else
                    {
                        string previus2 = previousItem.Substring(1, previousItem.Length - 1);
                        rs.Text += $"1.expresiones a multiplicar: {previus2}{currentItem}\n";
                    }
                    string method = Sign(previousItem, currentItem);
                    Remaster(previousItem, currentItem, method);
                }
            }

            StringBuilder builder = new StringBuilder();
            foreach (string c in expressionlist)
            {
                if (c.Length >= 1)
                {
                    if (!c.Contains('-') && !c.Contains('+') && !c.Contains('/') && !c.Contains('*'))
                    {
                        builder.Append("+");
                        builder.Append(c);
                    }
                    else
                    {
                        builder.Append(c);
                    }
                }

            }

            return builder.ToString();
        }

        public void Remaster(string a, string b, string method)
        {
            List<string> newList = new List<string>();
            StringBuilder builder = new StringBuilder();

            int cont = 0;
            foreach (string item in expressionlist)
            {
                if (item == a || item == b)
                {
                    if (cont == 0)
                    {
                        newList.Add(method);
                        cont = 1;
                    }
                }
                else
                {
                    newList.Add(item); 
                }
            }

            expressionlist = newList;
            Debug.WriteLine($"RESULTADO FINAL --> {string.Join(",", expressionlist)}");
            valuesLetter.Clear();
            Debug.WriteLine("");
            ConformationMultiplication(expressionlist);
        }

        public string Sign(string a, string b)
        {
            palabras(a);
            palabras(b);
            string newab = a + b;

            StringBuilder builder = new StringBuilder();
            List<int> list = new List<int>();

            for (int i = 0; i <= newab.Length - 1; i++)
            {
                if (digits.Contains(newab[i]) || newab[i] == '+' || newab[i] == '-')
                {
                    builder.Append(newab[i]);
                }
                else
                {
                    if (!string.IsNullOrEmpty(builder.ToString()))
                    {
                        list.Add(int.Parse(builder.ToString()));
                        builder.Clear();
                    }
                }
            }

            string valor = builder.ToString();
            if (!string.IsNullOrEmpty(valor))
            {
                list.Add(int.Parse(valor));
            }
            Debug.WriteLine($"builder a checar: {valor}");
            Debug.WriteLine($"variable a chekar: {newab}");
            Debug.WriteLine($"los numeros que se deben multiplicar tontolon = {string.Join(",", list)}");
            rs.Text += $"2.numeros a multiplicar: {string.Join(",", list)}\n";
            return ResultMultiplicationAlge(list, a, b);
        }

        public string ResultMultiplicationAlge(List<int> lista, string a, string b)
        {
            Dictionary<char, int> letterVallue = valuesLetter.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            rs.Text += $"3.letras con sus exponentes: {string.Join(",", letterVallue)}\n";
            int multiplicated = 1;

            foreach (int c in lista)
            {
                multiplicated *= c;
            }


            foreach (char c in letterVallue.Keys)
            {
                builder.Append(c);
                if (letterVallue[c] >= 2)
                {
                    builder.Append(letterVallue[c]);
                }
            }



            foreach (char c in builder.ToString())
            {
                if (superScriptDict.ContainsKey(c))
                {
                    builder2.Append(superScriptDict[c].ToString());
                }
                else
                {
                    builder2.Append(c);
                }
            }

            if (a[0] == '/' || a[0] == '*')
            {
                return a[0] + multiplicated.ToString() + builder2.ToString();
            }

            return multiplicated.ToString() + builder2.ToString(); ;
        }

        public void palabras(string a)
        {
            List<char> lista = new List<char>();

            for (int i = 0; i <= a.Length - 1; i++)
            {
                if (alphabet.Contains(a[i]) && !lista.Contains(a[i]))
                {
                    lista.Add(a[i]);
                    ContValueLetter(a[i], a);
                }
            }
        }

        public void ContValueLetter(char x, string a)
        {
            for (int i = 0; i <= a.Length - 1; i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("1");

                if (a[i].Equals(x))
                {
                    for (int j = i + 1; j <= a.Length - 1; j++)
                    {
                        if (SuperScriptMap.ContainsKey(a[j]))
                        {
                            if (builder.ToString().Equals("1"))
                            {
                                builder.Clear();
                            }
                            builder.Append(SuperScriptMap[a[j]]);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (valuesLetter.ContainsKey(x))
                    {
                        int value = valuesLetter[x];
                        valuesLetter.Remove(x);
                        valuesLetter.Add(x, value + int.Parse(builder.ToString()));
                    }
                    else
                    {
                        valuesLetter.Add(x, int.Parse(builder.ToString()));
                    }
                }
            }
        }
    }
}
