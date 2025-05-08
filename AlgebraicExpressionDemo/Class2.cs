using System.Diagnostics;
using System.Text;

namespace AlgebraicExpressionDemo
{
    class Class2
    {
        Editor editor_;
        List<string> lista = new List<string>();
        public char[] signs = { '+', '-' };
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        MainPage multiplicate = new MainPage();
        int cont = 1;
        public Class2(Editor editor) 
        {
            editor_ = editor;
            editor_.Text += $"-----------------------------------------------------\n ";
            editor_.Text += $"                             3.suma\n";
        }
        public void SumaExpression(string expression)
        {
            Debug.WriteLine($"expression -> {expression}");
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i <= expression.Length - 1; i++)
            {
                if (signs.Contains(expression[i]))
                {
                    if (expression[i] == '*' && expression[i + 1] == '-' || expression[i] == '/' && expression[i + 1] == '-')
                    {
                        lista.Add(builder.ToString());
                        builder.Clear();
                        builder.Append(expression[i]);
                        builder.Append(expression[i + 1]);
                        i += 1;
                    }
                    else
                    {
                        lista.Add(builder.ToString());
                        builder.Clear();
                        builder.Append(expression[i]);
                    }
                }
                else
                {
                    builder.Append(expression[i]);
                }
            }

            lista.Add(builder.ToString());
            Debug.WriteLine($"lista en suma class - > {string.Join(",", lista)}");
            NumberExpression();
        }

        public void NumberExpression()
        {
            for (int i = 0; i <= lista.Count - 1; i++)
            {
                if (!lista.ElementAt(i).Contains("/"))
                {
                    if (lista.ElementAt(i).Contains("+") || lista.ElementAt(i).Contains("-"))
                    {
                        LettersAndNums(lista.ElementAt(i));
                    }
                }
            }

            Debug.WriteLine($"dictionary = {string.Join(",", dictionary)}");
            editor_.Text += $"coeficientes y sus variables {string.Join(",", dictionary)}\n";
            Aggregate();
        }

        public void LettersAndNums(string operation)
        {
            StringBuilder numbers = new StringBuilder();
            StringBuilder letters = new StringBuilder();

            foreach (char c in operation)
            {
                if (signs.Contains(c) || multiplicate.digits.Contains(c))
                {
                    numbers.Append(c);
                }
                else
                {
                    letters.Append(c);
                }
            }

            if (numbers.Length == 1) 
            {
                numbers.Append("1");
            }

            Debug.WriteLine($"{numbers},{letters}");

            if (dictionary.ContainsKey(letters.ToString()))
            {
                dictionary[letters.ToString()] += int.Parse(numbers.ToString());
            }
            else
            {
                dictionary.Add(letters.ToString(), int.Parse(numbers.ToString()));
            }
        }

        public void Aggregate()
        {
            List<string> listNew = new List<string>();
            foreach (string c in lista)
            {
                if (c.Contains("-") || c.Contains("+"))
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i <= c.Length - 1; i++)
                    {
                        if (!signs.Contains(c[i]) && !multiplicate.digits.Contains(c[i]))
                        {
                            sb.Append(c[i]);
                        }
                    }

                    if (dictionary.ContainsKey(sb.ToString()))
                    {
                        string x = dictionary[sb.ToString()].ToString() + sb.ToString();
                        if (!listNew.Contains(x))
                        {
                            listNew.Add(x);
                        }
                    }
                    else
                    {
                        listNew.Add(c);
                    }
                }
                else
                {
                    listNew.Add(c);
                }
            }

            Debug.WriteLine($"nueva lista -> {string.Join(",", listNew)}");
            dictionary.Clear();
            lista.Clear();
            ResultDefinitive(listNew);
        }

        public void ResultDefinitive(List<string> UltimateLista)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string c in UltimateLista)
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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"resultado horneado: {builder.ToString()}");
            editor_.Text += $"RESULTADO: {builder.ToString()}\n";
            editor_.Text += $"-----------------------------------------------------\n";
            Console.ResetColor();
        }
    }
}
