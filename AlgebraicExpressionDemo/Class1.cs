using System.Diagnostics;
using System.Text;

namespace AlgebraicExpressionDemo
{
    internal class Class1
    {
        private Editor editor_;
        MainPage multiplicate = new MainPage();
        List<string> Resultados = new List<string>();
        SortedDictionary<char, int> lettervalue = new SortedDictionary<char, int>();
        List<string> lista = new List<string>();
        List<string> builderList = new List<string>();
        string resultFinal = "";
        public int lop = 1;
        Class2 SumaClass;
        public Class1(Editor editor)
        {
            editor_ = editor ?? throw new ArgumentNullException(nameof(editor));
        }

        public void Add(string expression)
        {
            editor_.Text += $"-----------------------------------------------------\n";
            editor_.Text += $"                             2.division\n";
            Divition(expression);
        }

        public void Divition(string expression)
        {
            try
            {
                if (!expression.Contains("/"))
                {
                    editor_.Text += $"1.RESULTADO: {expression}\n";
                    SumaClass = new Class2(editor_);
                    SumaClass.SumaExpression(expression);
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i <= expression.Length - 1; i++)
                    {

                        if (multiplicate.operators.Contains(expression[i]))
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
                    Debug.WriteLine($"lista en Division class - > {string.Join(",", lista)}");
                    Extraction(lista);
                }
            }
            catch (Exception e) 
            {
                editor_.Text = "Sintaxis erronea";
            }
        }

        public void Extraction(List<string> lista)
        {
            for (int i = 0; i <= lista.Count - 1; i++)
            {
                if (lista.ElementAt(i).Contains("/") && !builderList.Contains(lista.ElementAt(i - 1) + lista.ElementAt(i)))
                {

                    ExtraccionSign(lista.ElementAt(i - 1));
                    ExtraccionSign(lista.ElementAt(i));
                    Debug.WriteLine($"lista -> {string.Join(",", Resultados)}");
                    Aggregate(i - 1, i);
                }
            }

            if (lop == 1)
            {
                lop = 5;
                editor_.Text += $"4.RESULTADO: {resultFinal}\n";
                Debug.WriteLine($"se abrio lo de Suma");
                SumaClass = new Class2(editor_);
                SumaClass.SumaExpression(resultFinal);
            }
        }

        public void Aggregate(int a, int b)
        {
            StringBuilder top = new StringBuilder();
            StringBuilder down = new StringBuilder();
            Debug.WriteLine($"result de var = {Resultados.ElementAt(0)}, {Resultados.ElementAt(2)}");
            editor_.Text += $"1.numeros a dividir: {Resultados.ElementAt(0)}, {Resultados.ElementAt(2)}\n";
            down.Append("/");
            var result = int.Parse(Resultados.ElementAt(0)) % int.Parse(Resultados.ElementAt(2));
            int mcd = MCD(int.Parse(Resultados.ElementAt(0)), int.Parse(Resultados.ElementAt(2)));

            if (result == 0)
            {
                top.Append(int.Parse(Resultados.ElementAt(0)) / int.Parse(Resultados.ElementAt(2)));
            }
            else if (result >= 1 && mcd >= 2)
            {
                top.Append(int.Parse(Resultados.ElementAt(0)) / mcd);
                down.Append(int.Parse(Resultados.ElementAt(2)) / mcd);
            }
            else
            {
                top.Append(int.Parse(Resultados.ElementAt(0)));
                down.Append(int.Parse(Resultados.ElementAt(2)));
            }

            AddValueExponent(Resultados.ElementAt(1));
            AddValueExponent(Resultados.ElementAt(3));

            foreach (char c in lettervalue.Keys)
            {
                if (Resultados.ElementAt(1).Contains(c) && lettervalue[c] >= 1)
                {

                    top.Append(c);
                    if (lettervalue[c] != 1)
                    {
                        top.Append(StringOfValue(lettervalue[c].ToString()));
                    }
                }
                else if (lettervalue[c] < 0 || !Resultados.ElementAt(1).Contains(c))
                {
                    down.Append(c);
                    if (lettervalue[c] != 1)
                    {
                        down.Append(StringOfValue(lettervalue[c].ToString()));
                    }

                }
            }

            string expressionSimplificate = "";
            if (down.ToString() == "/")
            {
                Debug.WriteLine($"resultado final -> {top}");
                expressionSimplificate = top.ToString();
            }
            else
            {
                Debug.WriteLine($"resultado final -> {top}{down}");
                expressionSimplificate = top.ToString() + down.ToString();
            }

            Debug.WriteLine($"resultado de la division -> {result}");
            Debug.WriteLine($"valor de letras -> {string.Join(",", lettervalue)}");
            editor_.Text += $"3.valor de coeficientes y variables -> {string.Join(",", lettervalue)}\n";
            resultFinal = ResultRemaster(a, b, expressionSimplificate);
            Divition(resultFinal);
        }

        public string ResultRemaster(int i, int j, string expression)
        {
            StringBuilder sb = new StringBuilder();
            List<string> listaNew = new List<string>();
            int turn = 0;
            for (int a = 0; a <= lista.Count - 1; a++)
            {
                if (a == i && turn == 0 || a == j && turn == 0)
                {
                    listaNew.Add(expression);
                    turn = 1;
                }
                else if (a != i && a != j)
                {
                    listaNew.Add(lista[a]);
                }
            }

            foreach (string c in listaNew)
            {
                if (!string.IsNullOrEmpty(c))
                {
                    if (!c[0].ToString().Equals("-") && !c[0].ToString().Equals("+") && !c[0].ToString().Equals("/") && !c[0].ToString().Equals("*"))
                    {
                        sb.Append("+");
                        sb.Append(c);
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }

                if (c.Equals(expression))
                {
                    if (!c[0].ToString().Equals("-") && !c[0].ToString().Equals("+") && !c[0].ToString().Equals("/") && !c[0].ToString().Equals("*"))
                    {
                        builderList.Add("+" + c);
                    }
                    else
                    {
                        builderList.Add(c);
                    }
                }
            }

            Debug.WriteLine($"final de la lista remaster = {string.Join(",", listaNew)}");
            Debug.WriteLine($"final sb = {sb.ToString()}");
            Resultados.Clear();
            lettervalue.Clear();
            lista.Clear();
            return sb.ToString();
        }

        public string StringOfValue(string expresion)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i <= expresion.Length - 1; i++)
            {
                if (multiplicate.superScriptDict.ContainsKey(expresion[i]))
                {
                    builder.Append(multiplicate.superScriptDict[expresion[i]]);
                }
            }

            return builder.ToString();
        }

        public void AddValueExponent(string expression)
        {

            for (int i = 0; i <= expression.Length - 1; i++)
            {
                if (multiplicate.alphabet.Contains(expression[i]))
                {
                    StringBuilder builder = new StringBuilder();
                    for (int j = i + 1; j <= expression.Length - 1; j++)
                    {
                        if (multiplicate.SuperScriptMap.ContainsKey(expression[j]))
                        {
                            builder.Append(multiplicate.SuperScriptMap[expression[j]]);
                        }
                        else { break; }
                    }

                    if (string.IsNullOrEmpty(builder.ToString()))
                    {
                        if (lettervalue.ContainsKey(expression[i]))
                        {
                            int val = lettervalue[expression[i]];
                            lettervalue.Remove(expression[i]);
                            lettervalue.Add(expression[i], val - 1);
                        }
                        else
                        {
                            lettervalue.Add(expression[i], 1);
                        }
                    }
                    else
                    {
                        if (lettervalue.ContainsKey(expression[i]))
                        {
                            int val = lettervalue[expression[i]];
                            lettervalue.Remove(expression[i]);
                            lettervalue.Add(expression[i], val - int.Parse(builder.ToString()));
                        }
                        else
                        {
                            lettervalue.Add(expression[i], int.Parse(builder.ToString()));
                        }
                    }
                }
            }
        }

        public void ExtraccionSign(string a)
        {
            StringBuilder numbers = new StringBuilder();
            StringBuilder signexponents = new StringBuilder();

            for (int i = 0; i <= a.Length - 1; i++)
            {
                if (multiplicate.digits.Contains(a[i]) || a[i] == '-')
                {
                    numbers.Append(a[i]);
                }
                else if (multiplicate.alphabet.Contains(a[i]) || multiplicate.SuperScriptMap.ContainsKey(a[i]))
                {
                    signexponents.Append(a[i]);
                }
            }

            Resultados.Add(numbers.ToString());
            Resultados.Add(signexponents.ToString());
        }

        static int MCD(int a, int b)
        {
            // Si alguno de los números es 0, devolvemos el valor absoluto del otro número
            if (a == 0 || b == 0)
            {
                return Math.Abs(a + b);  // Esto garantiza que si uno es 0, el resultado sea el valor absoluto del otro
            }

            while (b != 0)
            {
                int temp = b;
                b = a % b;  // El residuo de a dividido por b
                a = temp;   // Asignamos b a a
            }

            return Math.Abs(a);  // El MCD siempre debe ser positivo
        }
    }
}
