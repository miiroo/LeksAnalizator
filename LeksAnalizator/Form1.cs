using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




/*
procedure SetDisplay(R: Real);
var  S: string[63];
begin
  Str(R: 0: 10, S);
  if S[1] <> '-' then Sign := ' ' else
  begin
    Delete(S, 1, 1);    Sign := '-';
  end;
  if Length(S) > 15 + 1 + 10 then Error
  else
  begin
    while S[Length(S)] = '0' do Dec(S[0]);
    if S[Length(S)] = '.' then Dec(S[0]);
    Number := S;
  end; 
end;
*/

namespace LeksAnalizator
{

    public partial class Form1 : Form 
    {
        private int start = 0;
        private String exitMessage = "";
        private int exitCode = 0;
        private List<String> keyArray = new List<String>() { "procedure", "Real", "var", "string", "begin", "if", "then", "else", "end", "while", "do", "+", "<", ">", "=" };
        private List<String> delimArray = new List<String>() { ":", ";", "'", ":=", "[", "]", "(", ")", "," };
        private List<String> identArray = new List<String>();
        private List<String> constArray = new List<String>();

      //  private bool startParse = false;

        public Form1() {
            //Заполняем константные таблицы
            InitializeComponent();
            textBox2.Text = "if Length(S) > 15 + 1 + 10 then Error";
            label8.Text = "";
            try {
                for (int i = 0; i < keyArray.Count; i++) {
                    keyWordGrid1.Rows.Add(i, keyArray.ElementAt(i));
                }
                for (int i = 0; i < delimArray.Count; i++) {
                    litteralsGrid.Rows.Add(i, delimArray.ElementAt(i));
                }
            }
            catch (Exception e) { throw e; }
        }

    
        



        private void button1_Click(object sender, EventArgs e) {
            start = 0;
            identifierGrid.Rows.Clear();
            logGrid.Rows.Clear();
            constantGrid.Rows.Clear();
            exitCode = 0;
            identArray.Clear();
            constArray.Clear();

            int rowNumber = 0;
            String currentRowStr = "";
            String tableType = "";
            int idInTable = 0;
            int logID = 0;
            //      resultLabel.Text = textBox1.Lines.Length.ToString();
            char currentLetter;
            String getWord = "";
            //Обход текста по строкам
            while (textBox1.Lines.Length > rowNumber && exitCode != 1) {
                currentRowStr = textBox1.Lines[rowNumber].ToString();
                rowNumber++;
                //Обход строки
                for (int i = 0; i < currentRowStr.Length && exitCode != 1; i++) {
                    exitCode = 0;
                    getWord = "";
                    currentLetter = currentRowStr[i];
                    if (!currentLetter.ToString().Equals(" ")) {
                        //Если начали с буквы
                        if (Char.IsLetter(currentLetter)) {
                            getWord += currentLetter;
                            i++;
                            if(i < currentRowStr.Length) currentLetter = currentRowStr.ElementAt(i);
                            while ((!currentLetter.Equals(' ')) && (Char.IsLetter(currentLetter) || Char.IsDigit(currentLetter)) && i < currentRowStr.Length) {
                                getWord += currentLetter;
                                i++;
                                if (i < currentRowStr.Length) currentLetter = currentRowStr.ElementAt(i);
                                else exitCode = 2;
                            }
                            i--;
                            //Проврка слова по таблицам 
                            if (currentLetter.Equals(' ') || i+1 >= currentRowStr.Length) {
                                if (keyArray.Contains(getWord)) {
                                    tableType = "key";
                                    idInTable = keyArray.IndexOf(getWord);
                                    exitCode = 2;
                                }
                                else {
                                    if (identArray.Contains(getWord)) {
                                        tableType = "id";
                                        idInTable = identArray.IndexOf(getWord);
                                        exitCode = 2;
                                    }
                                    else {
                                        identArray.Add(getWord);
                                        tableType = "id";
                                        idInTable = identArray.IndexOf(getWord);
                                        exitCode = 2;
                                    }
                                }
                            }
                            else {
                                //Если делиметр существует, значит проверям слово по таблицам 
                                if (delimArray.Contains(currentLetter.ToString()) && exitCode != 2) {

                                    if (keyArray.Contains(getWord)) {
                                        tableType = "key";
                                        idInTable = keyArray.IndexOf(getWord);
                                        exitCode = 2;
                                    }
                                    else {
                                        if (identArray.Contains(getWord)) {
                                            tableType = "id";
                                            idInTable = identArray.IndexOf(getWord);
                                            exitCode = 2;
                                        }
                                        else {
                                            identArray.Add(getWord);
                                            tableType = "id";
                                            idInTable = identArray.IndexOf(getWord);
                                            exitCode = 2;
                                        }
                                    }
                                }
                                //Иначе выдаём ошибку о том, что не знаем такого символа
                                else {
                                    if (exitCode != 1) {
                                       
                                        exitMessage = "Can't recognize symbol in keyword or constant in row " + rowNumber.ToString();
                                        if (exitCode != 1) {
                                            if (rowNumber != 1) {
                                                for (int j = 0; j < rowNumber-1; j++) {
                                                    start += textBox1.Lines[j].Count()+2;
                                                }
                                            }
                                            start += i;
                                        }
                                        exitCode = 1;
                                    }
                                }


                            }
                        }
                        //Если начали с цифры
                        if (Char.IsDigit(currentLetter) && exitCode != 2) {
                            getWord += currentLetter;
                            i++;
                            if (i < currentRowStr.Length) currentLetter = currentRowStr[i];
                            while ((!currentLetter.Equals(' ')) && Char.IsDigit(currentLetter) && exitCode != 2 && i < currentRowStr.Length) {
                                getWord += currentLetter;
                                i++;
                                if (i < currentRowStr.Length) currentLetter = currentRowStr[i];
                            }
                            i--;
                            //Если после числа пробел - число константа
                            if (currentLetter.Equals(' ') || Char.IsLetter(currentLetter) && exitCode != 2) {
                                if (constArray.Contains(getWord)) {
                                    tableType = "const";
                                    idInTable = constArray.IndexOf(getWord);
                                    exitCode = 2;
                                }
                                else {
                                    constArray.Add(getWord);
                                    tableType = "const";
                                    idInTable = constArray.IndexOf(getWord);
                                    exitCode = 2;
                                }
                            }
                            //Идёт делиметр или ключевое слово, число константа
                            if (delimArray.Contains(currentLetter.ToString()) || keyArray.Contains(currentLetter.ToString()) && exitCode != 2) {
                                if (constArray.Contains(getWord)) {
                                    tableType = "const";
                                    idInTable = constArray.IndexOf(getWord);
                                    exitCode = 2;
                                }
                                else {
                                    constArray.Add(getWord);
                                    tableType = "const";
                                    idInTable = constArray.IndexOf(getWord);
                                    exitCode = 2;
                                }
                            } 
                            else {
                                if (exitCode != 2) {
                                    //Такого делиметра не существует для нас

                                    exitMessage = "Can't recognize symbol in row " + rowNumber.ToString();

                                    if (exitCode != 1) {
                                        if (rowNumber != 1) {
                                            for (int j = 0; j < rowNumber - 1; j++) {
                                                start += textBox1.Lines[j].Count();
                                            }
                                        }
                                        start += i;
                                    }
                                    exitCode = 1;
                                }
                            }
                        }


                        //Если начали с делиметра
                        if (!Char.IsLetterOrDigit(currentLetter) && !currentLetter.ToString().Equals(" ") && exitCode != 2) {
                            //Если символ - ключевое слово (+, -, *, /)
                            if (keyArray.Contains(currentLetter.ToString())) {
                                getWord = currentLetter.ToString();
                                tableType = "key";
                                idInTable = keyArray.IndexOf(getWord);
                                exitCode = 2;
                            }

                            if (delimArray.Contains(currentLetter.ToString()) && exitCode != 2) {
                                //если делиметр ' - строковая константа
                                //если : проверить на двойной
                                if (currentLetter.Equals('\'')) {
                                    //добавляем в лог, потом ищем всю константу
                                    tableType = "delim";
                                    idInTable = delimArray.IndexOf("'");
                                    logGrid.Rows.Add(logID, idInTable.ToString(), "'", tableType, rowNumber);
                                    logID++;


                                    getWord += currentLetter;
                                    i++;
                                    if (i < currentRowStr.Length) currentLetter = currentRowStr[i];
                                    while (!currentLetter.Equals('\'')) {
                                        getWord += currentLetter;
                                        i++;
                                        currentLetter = currentRowStr[i];
                                    }
                                    getWord += currentLetter;
                                    i++;

                                    //Добавляем в лог ещё один апостроф, потом всю константу

                                    tableType = "delim";
                                    idInTable = delimArray.IndexOf("'");
                                    logGrid.Rows.Add(logID, idInTable.ToString(), "'", tableType, rowNumber);
                                    logID++;

                                    if (constArray.Contains(getWord)) {
                                        tableType = "const";
                                        idInTable = constArray.IndexOf(getWord);
                                        exitCode = 2;
                                    }
                                    else {
                                        constArray.Add(getWord);
                                        tableType = "const";
                                        idInTable = constArray.IndexOf(getWord);
                                        exitCode = 2;
                                    }
                                }
                                if (currentLetter.ToString().Equals(":") && exitCode != 2) {
                                    if (currentRowStr[i + 1].Equals("=")) {
                                        getWord = ":=";
                                        i++;
                                        tableType = "delim";
                                        idInTable = delimArray.IndexOf(":=");
                                        exitCode = 2;
                                    }
                                    else {
                                        getWord += currentLetter;
                                        tableType = "delim";
                                        idInTable = delimArray.IndexOf(":");
                                        exitCode = 2;
                                    }
                                }
                                if (!currentLetter.ToString().Equals(":") && !currentLetter.ToString().Equals("'") && exitCode != 2) {
                                    getWord += currentLetter;
                                    tableType = "delim";
                                    idInTable = delimArray.IndexOf(currentLetter.ToString());
                                    exitCode = 2;
                                }
                            }
                            else {
                                if (exitCode != 2) {
                                    //Такого делиметра не существует для нас

                                    exitMessage = "Can't recognize symbol in row " + rowNumber.ToString();

                                    if (exitCode != 1) {
                                        if (rowNumber != 1) {
                                            for (int j = 0; j < rowNumber-1; j++) {
                                                start += textBox1.Lines[j].Count();
                                            }
                                        }
                                        start += i;
                                    }
                                    exitCode = 1;
                                }
                            }
                        }
                        if (exitCode != 1) {
                            logGrid.Rows.Add(logID, idInTable.ToString(), getWord, tableType, rowNumber);
                            logID++;
                        }
                    }
                }
            }
            if (exitCode != 1) {
                for (int i = 0; i < constArray.Count; i++) {
                    constantGrid.Rows.Add(i, constArray.ElementAt(i));
                }
                for (int i = 0; i < identArray.Count; i++) {
                    identifierGrid.Rows.Add(i, identArray.ElementAt(i));
                }
                resultLabel.Text = "SUCCESS";
               // startParse = true;
            }
            else {
                resultLabel.Text = exitMessage;

                textBox1.Select(start+1, 1);
                textBox1.Focus();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////LAB2////////////////////////////////////////////////////
        // if (Length(S) > 15 + 1 + 10) then Error
        private void button2_Click(object sender, EventArgs e) {
            string str = textBox2.Text;
          //  Console.WriteLine("CODE:");
          //  Console.WriteLine(str);
            string statement = "";
            int brackets = 0;

            //there is IF and THEN
            if (findIfThen(str)) {
                //get statement between IF and THEN
                int posStart = str.IndexOf("if") + 2;
                int posEnd = str.IndexOf("then");
                for (int i = posStart; i < posEnd; i++) {
                    if (str[i] == '(') brackets++;
                    if (str[i] == ')') brackets--;
                    statement += str[i];
                }

                //check expression is correct
                if (checkExpr(statement) && brackets == 0) {
                    string part = "";
                    for (int i = posEnd + 4; i < str.Length; i++) part += str[i];
                    if (checkGrammar(part)) label8.Text = "Success";
                    else label8.Text = "Error. Missing statement after THEN.";
                }
                else label8.Text="Error. Wrong bool expression.";
            }
            else {
                label8.Text ="Error. IF or THEN not found.";
            }
        }

        //find if and then. Return TRUE if all is ok in other case return FALSE
        private bool findIfThen(string str) {
            string word = "";
            bool ifFounded = false;
            bool thenFounded = false;
            for (int i = 0; i < str.Length; i++) {
                word = "";
                while (i < str.Length && !delimArray.Contains(str[i].ToString()) && str[i] != ' ') {
                    word += str[i];
                    i++;
                }
                //there should be one IF and one THEN in other case it's error.
                if (word == "if" && !ifFounded) {
                    //symbol after IF should be ( or space
                    if (str[i] == '(' || str[i] == ' ') ifFounded = true;
                    else return false;
                }
                else {
                    if (word == "if") return false;
                }

                if (word == "then" && !thenFounded) {
                    //symbol before THEN should be ) or space
                    //symbol after THEN should be space
                    if ((str[str.IndexOf("then") - 1] == ' ' || str[str.IndexOf("then") - 1] == ')') && str[str.IndexOf("then") + 4] == ' ') thenFounded = true;
                    else return false;
                }
                else {
                    if (word == "then") return false;
                }

            }
            if (ifFounded && thenFounded) return true;
            return false;
        }

        //chech boolean expression. Return TRUE if all is ok in other case return FALSE
        private bool checkExpr(string str) {
            string part = "";
            string part2 = "";
            //check that there is OR/AND ///////////
            if (str.Contains("or")) {
                //check it's not in some word
                if (str.IndexOf("or") + 2 < str.Length && (str[str.IndexOf("or") - 1] != ')' && str[str.IndexOf("or") - 1] != ' ') && (str[str.IndexOf("or") + 2] != '(' && str[str.IndexOf("or") + 2] != ' ')) {
                    //there is no OR. We should check boolean grammar
                    if (boolGramar(str)) return true;
                    else return false;

                }
                else {
                    if (!(str.IndexOf("or") + 2 < str.Length)) return false;
                    //there is OR. We should divide to two parts and check both of them
                    int posS = str.IndexOf("or");
                    for (int i = 0; i < posS; i++) part += str[i];
                    posS += 2;
                    for (int i = posS; i < str.Length; i++) part2 += str[i];

                    if (checkExpr(part))
                        if (checkExpr(part2)) return true;
                        else return false;
                    else return false;
                }
            }

            if (str.Contains("and")) {
                //check it's not in some word
                if (str.IndexOf("and") + 3 < str.Length && (str[str.IndexOf("and") - 1] != ')' && str[str.IndexOf("and") - 1] != ' ') && (str[str.IndexOf("and") + 3] != '(' && str[str.IndexOf("and") + 3] != ' ')) {
                    //there is no AND. We should check boolean grammar
                    if (boolGramar(str)) return true;
                    else return false;
                }
                else {
                    if (!(str.IndexOf("and") + 3 < str.Length)) return false;
                    //there is AND. We should divide to two parts and check both of them
                    int posS = str.IndexOf("and");
                    for (int i = 0; i < posS; i++) part += str[i];
                    posS += 3;
                    for (int i = posS; i < str.Length; i++) part2 += str[i];

                    if (checkExpr(part))
                        if (checkExpr(part2)) return true;
                        else return false;
                    else return false;
                }
            }
            ///////////OR/AND CHEKING END//////////////

            //there were no OR/AND
            if (boolGramar(str)) return true;
            return false;
        }

        //cheking how properly bool statement is
        //return TRUE if all is ok in other case return FALSE
        //bool statement is smth </>/<=/>=/= smth
        //smth = some function or math operations
        private bool boolGramar(string str) {
            bool delimF = false;
            string part = "";
            bool part1 = false;
            bool part2 = false;
            //get parts till and after delim
            for (int j = 0; j < str.Length; j++) {
                part = "";
                while (j < str.Length && str[j] != '<' && str[j] != '>' && str[j] != '=') {
                    part += str[j];
                    j++;
                }
                // we found delimetr and it's alone
                if (!delimF) {
                    if (!(j < str.Length))
                        if (checkGrammar(part)) return true;
                        else return false;
                    switch (str[j]) {
                        case '<':
                            if (str[j + 1] == '=' || str[j + 1] == '>') j++;
                            part1 = checkGrammar(part);
                            delimF = true;
                            break;
                        case '>':
                            if (str[j + 1] == '=') j++;
                            part1 = checkGrammar(part);
                            delimF = true;
                            break;
                        case '=':
                            part1 = checkGrammar(part);
                            delimF = true;
                            break;
                    }
                }
                //we found delimetr but we have already found it
                //or it's end of string
                else {
                    //we found delimetr
                    if (j < str.Length && (str[j] == '<' || str[j] == '>' || str[j] == '=')) return false;
                    part2 = checkGrammar(part);
                }
            }
            if (part1 && part2) return true;
            return false;
        }

        //we have to check statement grammar
        //it could be fuction or math operations
        //but in our case we just check that there is smth
        private bool checkGrammar(string str) {
            for (int i = 0; i < str.Length; i++) {
                if (str[i] != ' ') return true;
            }
            return false;
        }
    }
}
