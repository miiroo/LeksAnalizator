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
        private List<String> keyArray = new List<String>() { "procedure", "Real", "var", "string", "begin", "if", "then", "else", "end", "while", "do",  "=", "+", "<", ">", "*", "-", "/" };
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
            if (fastAn(str)) {
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
                        if (checkGr(part)) label8.Text = "Success";
                        else label8.Text = "Error. Missing statement after THEN.";
                    }
                    else label8.Text = "Error. Wrong bool expression.";
                }
                else {
                    label8.Text = "Error. IF or THEN not found.";
                }

            }
        }

        private bool fastAn(string str) {
            int i = 0;
            while (i < str.Length) {
                if (Char.IsDigit(str[i]) || Char.IsLetter(str[i]) || delimArray.Contains(str[i].ToString()) || keyArray.Contains(str[i].ToString()) || str[i] == ' ') i++;
                else {
                    label8.Text = "Can't recognize that symbol: " + str[i];
                    return false;
                }
            }
            return true;
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
                    
                    //check is it alone statement or id
                    if (!(j < str.Length))
                        if (checkGr(part)) return true;
                        else return false;

                    switch (str[j]) {
                        case '<':
                            if (str[j + 1] == '=' || str[j + 1] == '>') j++;
                            part1 = checkGr(part);
                            delimF = true;
                            break;
                        case '>':
                            if (str[j + 1] == '=') j++;
                            part1 = checkGr(part);
                            delimF = true;
                            break;
                        case '=':
                            part1 = checkGr(part);
                            delimF = true;
                            break;
                    }
                }
                //we found delimetr but we have already found it
                //or it's end of string
                else {
                    //we found delimetr
                    if (j < str.Length && (str[j] == '<' || str[j] == '>' || str[j] == '=')) return false;
                    part2 = checkGr(part);
                }
            }
            if (part1 && part2) return true;
            return false;
        }


        //universal way
        private bool checkGr(string str) {
            if (str == "") return false;
            int j = 0;
            //some cleaning from trash (, ) and spaces
            while (j < str.Length && str[j] == ' ' || str[j] == '(')
                str = str.Remove(j, 1);

            j = str.Length - 1;
            while (j > 0 && str[j] == ' ') {
                str = str.Remove(j, 1);
                j = str.Length - 1;
            }

            int countOpen = 0;
            bool strConst = false;
            for (int i = 0; i < str.Length; i++) {
                if (!strConst) {
                    if (str[i] == '(') countOpen++;
                    if (str[i] == ')' && countOpen <= 0) {
                        str = str.Remove(i, 1);
                        i--;
                    }
                    if (str[i] == ')' && countOpen > 0) countOpen--;
                }
                if (str[i].ToString() == "'") strConst = !strConst;
            }
            //////////////end of cleaning////////////////////

            //part is <numconst>
            if (checkNum(str)) return true;
            //part is <strconst>
            if (checkStr(str)) return true;
            //part is <func>
            if (checkFunc(str)) return true;
            //part is <assign>
            if (checkAssign(str)) return true;
            //part is <id>
            if (checkId(str)) return true;
            return false;
        }


        //<numconst> := <number><operator><numconst> | <number>
        //<operator> := +|-|*|/
        //<number> := <number><digit> | <digit>
        //<digit> := 0..9
        private bool checkNum(string str) {
            if (str == "") return false;

            bool rowNum = false;
            bool rowOp = false;
            for (int i = 0; i < str.Length; i++) {
                if (Char.IsLetter(str[i])) return false;

                //it's digit
                if (char.IsDigit(str[i])) {
                    if (rowOp) rowOp = false;

                    if (!rowNum) {
                        rowNum = true;
                        while (i < str.Length && Char.IsDigit(str[i])) i++;
                        i--;
                    }
                    else return false;
                }

                //it's operator
                if (keyArray.Contains(str[i].ToString())) {
                    if (!rowNum) return false;
                    rowNum = false;
                    if (!rowOp) rowOp = true;
                    else return false;
                }
            }
            if (rowOp) return false;
            return true;
        }

        //<strconst> := '<str>'
        //<str> := <str><letter> | <letter>
        //<letter> := a..z | A..Z | 0..9 | space
        private bool checkStr(string str) {
            if (str == "") return false;

            bool findOne = false;
            bool findSecond = false;

            for (int i = 0; i < str.Length; i++) {
                if (str[i] != ' ' || str[i].ToString() != "'") {
                    if (!findOne) return false;
                    if (findOne && findSecond) return false;
                }
                if (str[i].ToString() == "'") {
                    if (!findOne) findOne = true;
                    else {
                        if (!findSecond) findSecond = true;
                        else return false;
                    }
                }
            }
            return true;
        }

        //<func> := <id>(<arg>) | <id>()
        //<arg> := <argum> | <arg>,<argum>
        //<argum> := <part>
        private bool checkFunc(string str) {
            if (str == "") return false;

            //check func name is correct function name
            if (Char.IsDigit(str[0])) return false;
            int j = 0;
            string argum = "";
            //find func (
            while (j < str.Length && str[j] != '(') j++;
            j++;
            int countOpen = 1;
            bool getArg = false;
            bool space = false;
            bool haveWord = false;
            bool notALone = false;
            bool getConst = false;
            //get argument in func(<arg>)
            while (j < str.Length && !getArg) {
                if (str[j].ToString() == "'") getConst = !getConst;
                if (!getConst) {
                    while (j < str.Length && str[j] != ',' && countOpen != 0) {
                        if (str[j] == '(') countOpen++;
                        if (str[j] == ')') countOpen--;
                        if (str[j] == ' ') {
                            if (haveWord) space = true;
                        }
                        else {
                            haveWord = true;
                            if (countOpen != 0) {
                                if (space) return false;
                            }
                        }
                        if (countOpen != 0) {
                            argum += str[j];
                            j++;
                        }
                    }
                    if (j < str.Length && str[j] == ',') notALone = true;
                    if (countOpen == 0) getArg = true;

                    if (notALone) {
                        if (!checkGr(argum)) return false;
                    }
                    else {
                        bool onlySpaces = true;
                        for (int i = 0; i < argum.Length; i++)
                            if (argum[i] != ' ') onlySpaces = false;
                        if (!onlySpaces)
                            if (!checkGr(argum)) return false;
                    }

                    haveWord = false;
                    argum = "";
                }
                j++;
            }
            if (j < str.Length) {
                for (int i = j; i < str.Length; i++)
                    if (str[i] != ' ') return false;
            }
            if (getConst) return false;
            if (!getArg) return false;
            return true;
        }

        //<assign> := <id> := <part>
        private bool checkAssign(string str) {
            if (str == "") return false;
            string word = "";
            int j = 0;
            while (j < str.Length && str[j] != ':') {
                word += str[j];
                j++;
            }
            if (j >= str.Length) return false;
            if (!checkId(word)) return false;
            if (str[j + 1] != '=') return false;
            j += 2;
            word = "";
            while (j < str.Length) {
                word += str[j];
            }
            if (!checkGr(word)) return false;

            return true;
        }

        //<id> := idd | <id>.idd | <func>.idd
        private bool checkId(string str) {
            if (str == "") return false;
            bool isFirst = true;

            bool isFunc = false;
            string word = "";
            for (int j = 0; j < str.Length; j++) {
                if (str[j] != '.') {
                    if (isFirst) {
                        if (Char.IsDigit(str[j])) return false;
                        isFirst = false;
                    }
                    if (str[j] == '(') isFunc = true;
                    word += str[j];
                }
                else {
                    isFunc = false;
                    if (!checkGr(word)) return false;
                    word = "";
                    isFirst = true;
                }
            }
            if (isFunc) return false;

            return true;
        }

    }
}
