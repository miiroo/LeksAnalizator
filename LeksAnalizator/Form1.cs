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
        private List<String> delimArray = new List<String>() { ":", ";", "'", ":=", "[", "]", "(", ")", ",", "." };
        private List<String> identArray = new List<String>();
        private List<String> constArray = new List<String>();
        private string errorMessage = "";
        private bool strongError = false;

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
        //if (a<b and
        //if a(b and
        private void button2_Click(object sender, EventArgs e) {
            string str = textBox2.Text;
            label8.Text = "";
            strongError = false;
            errorMessage = "";
            if (fastAn(str)) {
                string statement = "";
                int brackets = 0;
                if (fastAn(str)) {

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
                        bool just = false;
                        //check expression is correct
                        if (brackets == 0) {
                            if (checkBoolSt(statement)) {
                                string part = "";
                                for (int i = posEnd + 4; i < str.Length; i++) {
                                    if (str[i] != ';') part += str[i];
                                    else {
                                        i++;
                                        while (i < str.Length && str[i] ==' ') i++;
                                        if (i < str.Length) just = true;
                                        i = str.Length;
                                    }
                                }
                                if (checkState(part) && !just) {
                                    brackets = 0;
                                    for (int i = str.IndexOf(part); i < str.Length; i++) {
                                        if (str[i] == '(') brackets++;
                                        if (str[i] == ')') brackets--;
                                    }
                                    if (brackets == 0)  label8.Text = "Success";
                                    else {
                                        label8.Text = "Error massage - missing ( or )";
                                    }
                                }

                                else {
                                    label8.Text = "Error message -  wrong or missing statement after THEN.";
                                    label8.Text += "\n" + errorMessage;
                                }
                            }
                            else {
                                //   label8.Text ="Error message -  error in expression");
                                label8.Text = errorMessage;
                                label8.Text += "\nErro message - error in bool expression";
                            }
                        }
                        else {
                            if (brackets > 0) label8.Text ="Error message -  ) not found between IF and THEN (bool expression)";
                            if (brackets < 0) label8.Text ="Error message -  ( not found between IF and THEN (bool expression)";
                        }
                    }
                    else {
                        label8.Text += "\nError message -  some troubles with IF or THEN.";
                    }
                }
            }
        }


        //cleaning function. Returns string without additional spaces and brackets in it's start and end
        private string cleaning(string str) {
            if (str == "") {
                return "";
            }
            //postion of last (
            int posOpen = 0;
            while (posOpen < str.Length && str[posOpen] == ' ' || str[posOpen] == '(') {
                if (str[posOpen] == ' ')
                    str = str.Remove(posOpen, 1);
                else posOpen++;

                if (str == "") return "";
            }
            if (str == "") {
                return "";
            }
            posOpen--;
            if (posOpen == str.Length) return "";

            int posEnd = str.Length - 1;
            while (str != "" && posEnd > 0 && str[posEnd] == ' ' || str[posEnd] == ')') {
                if (str[posEnd] == ' ')
                    str = str.Remove(posEnd, 1);
                posEnd--;
                if (str == "") return "";
            }
            if (str == "") {
                return "";
            }
            posEnd++;
            if (posEnd == 0) return "";

            int brackets = 0;
            int lastPos = posOpen+1;
            if (posOpen >= 0 && posOpen < str.Length && str[posOpen] == '(') {

                while (posOpen >= 0) {
                    while (lastPos < str.Length && (str[lastPos] != ')' || brackets != 0)) {
                        if (str[lastPos] == '(') brackets++;
                        if (str[lastPos] == ')') brackets--;
                        lastPos++;
                    }

                    if (lastPos == str.Length) {
                        while (posOpen >= 0) {
                            str = str.Remove(posOpen, 1);
                            posOpen--;
                        }
                        return str;
                    }
                    posOpen--;
                    lastPos++;
                }

                posOpen = 0;
                posEnd = str.Length - 1;
                while (str[posOpen] == '(' && str[posEnd] == ')') {
                    str = str.Remove(posEnd, 1);
                    posEnd--;
                    str = str.Remove(posOpen, 1);
                }

            }
            else {
                brackets = 0;
                lastPos = posEnd - 1;
                if (posEnd < str.Length && posEnd >= 0 && str[posEnd] == ')') {
                    while (posEnd < str.Length) {
                        while (lastPos > 0 && (str[lastPos] != '(' || brackets != 0)) {
                            if (str[lastPos] == '(') brackets++;
                            if (str[lastPos] == ')') brackets--;
                            lastPos--;
                        }

                        if (lastPos == 0) {
                            while (posEnd < str.Length) {
                                str = str.Remove(posEnd, 1);
                            }
                            return str;
                        }
                        posEnd++;
                        lastPos--;
                    }

                    posOpen = 0;
                    posEnd = str.Length - 1;
                    while (str[posOpen] == '(' && str[posEnd] == ')') {
                        str = str.Remove(posEnd, 1);
                        posEnd--;
                        str = str.Remove(posOpen, 1);
                    }
                }
            }
            return str;
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
                    else {
                        label8.Text ="Error message -  IF not found.";
                        return false;
                    }
                }
                else {
                    if (word == "if") {
                        label8.Text ="Error message -  find IF but it's already exist.";
                        return false;
                    }
                }

                if (word == "then" && !thenFounded) {
                    //symbol before THEN should be ) or space
                    //symbol after THEN should be space
                    if (str.IndexOf("then") + 4 <= str.Length) {
                        if ((str[str.IndexOf("then") - 1] == ' ' || str[str.IndexOf("then") - 1] == ')') && str[str.IndexOf("then") + 4] == ' ') thenFounded = true;
                        else {
                            label8.Text = "Error message -  THEN not found.";
                            return false;
                        }
                    } 
                }
                else {
                    if (word == "then") {
                        label8.Text ="Error message -  THEN was found, but it's already exist";
                        return false;
                    }
                }

            }
            if (ifFounded && thenFounded) return true;
            label8.Text ="Error message -  IF and THEN not found.";
            return false;
        }


        //<bool st> ::= <expr> and/or <bool st> | <expr>
        private bool checkBoolSt(string str) {
           // str = cleaning(str);
            if (strongError) return false;
            string word1 = "";
            string word2 = "";
            bool foundAndOr = false;
            for (int i=0; i<str.Length; i++) {
                if (!foundAndOr) {
                    if (str[i] != ' ') word1 += str[i];
                    else {
                        if (word1 == "and" || word1 == "or") {
                            word1 = "";
                            foundAndOr = true;
                            if (!checkExpr(word2)) {
                                errorMessage += "\nError message - in " + str; //optional
                                return false;
                            }
                            word2 = "";
                        }
                        else {
                            word2 += str[i];
                            word2 += word1;
                            word1 = "";
                        }
                    }
                }
                else {
                    word1 += str[i];
                }
            }
            word2 += " ";
            word2 += word1;
            if (!foundAndOr) {
                if (!checkExpr(word2)) {
                    errorMessage += "\nError message - in " + str; //optional
                    return false;
                }
            }
            else {
                if (!checkBoolSt(word1)) {
                    errorMessage += "\nError message - in " + str; //optional
                    return false;
                } 
            }
            return true;
        }


        //<expr> ::= <smth> </>/=/<=/>= <smth>
        private bool checkExpr(string str) {
          //  str = cleaning(str);
            if (strongError) return false; 
            string word = "";
            bool findCompare = false;
            bool isConst = false;
            for (int i=0; i<str.Length; i++) {
                if (!isConst) {
                    if (str[i].ToString() == "'") isConst = true;
                    if (str[i] != '<' && str[i] != '>' && str[i] != '=') word += str[i];
                    else {
                        if (findCompare) {
                            strongError = true;
                            errorMessage = "\nError message - compare symbol already founded";
                            return false;
                        }

                        switch (str[i]) {
                            case '<':
                                findCompare = true;
                                if(i+1>=str.Length) {
                                    strongError = true;
                                    errorMessage += "\nError message - missing expression in "+str;
                                    return false;
                                }
                                if (str[i + 1] == '=' || str[i + 1] == '>') i++;
                                if(!checkSmth(word)) {
                                    errorMessage += "\nError message - in " + str; //optional
                                    return false;
                                }
                                
                                break;
                            case '>':
                                findCompare = true;
                                if (i + 1 >= str.Length) {
                                    strongError = true;
                                    errorMessage += "\nError message - missing expression in " + str;
                                    return false;
                                }
                                if (str[i + 1] == '=') i++;
                                if (!checkSmth(word)) {
                                    errorMessage += "\nError message - in " + str; //optional
                                    return false;
                                }
                                break;
                            case '=':
                                findCompare = true;
                                if (i + 1 >= str.Length) {
                                    strongError = true;
                                    errorMessage += "\nError message - missing expression in " + str;
                                    return false;
                                }
                                if (!checkSmth(word)) {
                                    errorMessage += "\nError message - in " + str; //optional
                                    return false;
                                }
                                break;
                        }
                        word = "";
                    }
                }
                else {
                    if (str[i].ToString() == "'") isConst = false;
                    word += str[i];
                }
            }
            if(!findCompare) {
                strongError = true;
                errorMessage += "\nError message - missing boolean expression in " + str;
                return false;
            }
            if(!checkSmth(word)) {
             //   errorMessage += "\nError message - in " + str; //optional
                return false;
            }
            return true;
        }

        //<smth> ::= <part> | <strconst>
        private bool checkSmth(string str) {
            if (strongError) return false;
            str = cleaning(str);
            if (str == "" ) {
                errorMessage += "\nError message - missing expression.";
                return false;
            }
            if (str[0].ToString() == "'" && str[str.Length - 1].ToString() == "'") return true;
            else {
                if (!checkPart(str)) {
                    errorMessage += "\nError message - in " + str; //optional
                    return false;
                }
            }
            return true;
        }


        //<part>::=<T> | <T>+<part> | <T>-<part>
        private bool checkPart(string str) {
            if (strongError) return false;
            str = cleaning(str);
            if (str == "") {
                strongError = true;
                errorMessage += "\nError message - missing statement";
                return false;
            }

            string word = "";
            int countBr = 0;
            bool exVar = true;
            int j = 0;
            while (exVar && j<str.Length) {
                if (str[j] == '(') { countBr++; }
                if (str[j] == ')') { countBr--; }

                if (str[j] == '+' || str[j] == '-') {
                    if (countBr == 0) {
                        if (!checkT(word)) {
                            return false;
                        }
                        word = "";
                    }
                    else word += str[j];
                }
                else word += str[j];
                j++;
            }
            if (!checkT(word)) {
               // errorMessage += "\nError message - in " + str; //optional
                return false;
            }
            return true;
        }

        //<T> ::= <F> | <F>*<F> | <F> / <F>
        private bool checkT(string str) {
            if (strongError) return false;
            str = cleaning(str);
            if (str == "") {
                strongError = true;
                errorMessage += "\nError message - missing statement";
                return false;
            }
            string word = "";
            int countBr = 0;
            bool exVar = true;
            int j = 0;
            while (exVar && j < str.Length) {
                if (str[j] == '(') { countBr++; }
                if (str[j] == ')') { countBr--; }

                if (str[j] == '*' || str[j] == '/') {
                    if (countBr == 0) {
                        if (!checkF(word)) {
                            return false;
                        }
                        word = "";
                    }
                    else word += str[j];
                }
                else word += str[j];
                j++;
            }
            if(!checkF(word)) {
               // errorMessage += "\nError message - in " + str;
                return false;
            }
            return true;
        }

        //<F> ::= <ID> | <func> | <number> | (<part>) 
        private bool checkF(string str) {
            if (strongError) return false;
            if (str == "") {
                strongError = true;
                errorMessage += "\nError message - missing statement";
                return false;
            }
            if (str[0] == '(' && str[str.Length-1] == ')') return checkPart(str);
            if (checkNumb(str)) return true;
            if (checkId(str)) return true;
            if (checkFunc(str)) return true;
            return false;
        }

        //<number> ::= digit<number>|digit
        private bool checkNumb(string str) {
            if (strongError) return false;
            if (str == "") {
                strongError = true;
                errorMessage += "\nError message - missing statement";
                return false;
            }
            bool numRow = false;
            bool getNumb = false;
            for (int i=0; i<str.Length; i++) {
                if (Char.IsDigit(str[i])) {
                    getNumb = true;
                    if (numRow) {
                        strongError = true;
                        errorMessage = "Error message - expected OPERATOR but "+str[i]+" was found";
                        return false;
                    }
                }
                if (str[i] == ' ') {
                    if (getNumb) numRow = true;
                }
                if (Char.IsLetter(str[i]) || delimArray.Contains(str[i].ToString())) {
                    if (numRow) {
                        strongError = true;
                        errorMessage = "Error message - expected OPERATOR but " + str[i] + " was found";
                        return false;
                    }
                    else return false;
                }
            }
            if (getNumb) return true;
            strongError = true;
            errorMessage = "Error message - missing statement";
            return false;
        }

        //<id>::=<id>.idd | idd
        private bool checkId(string str) {
            if (strongError) return false;
            if (str == "") {
                strongError = true;
                errorMessage += "\nError message - missing statement";
                return false;
            }
            //string word = "";
            bool isWord = false;
            for (int i=0; i<str.Length; i++) {
                if (Char.IsDigit(str[i]) && !isWord) {
                    strongError = true;
                    errorMessage += "\nError message - identifier/function can't start with digit in " + str;
                    return false;
                }
                if (Char.IsLetter(str[i])) {
                    isWord = true;
                }
                if (str[i] == '.') isWord = false;
                if (str[i] == '(') return false;
                if (delimArray.Contains(str[i].ToString()) && str[i] != '(' && str[i] != '.') {
                    strongError = true;
                    errorMessage += "\nError message - identifier/function can't consider current symbol " + str[i] + " in " +str;
                    return false;
                }
                if (str[i] == ' ') {
                    strongError = true;
                    errorMessage += "\nError message - expected ; in " + str;
                    return false;
                }
            }
            return true;
        }

        private bool checkFunc(string str) {
            if (strongError) return false;
            if (str == "") {
                strongError = true;
                errorMessage += "\nError: missing statement";
                return false;
            }

            bool isConst = false;
            string word = "";
            int j = 0;
            while (j < str.Length && str[j] != '(') {
                word += str[j];
                j++;
            }
            if (!checkId(word)) {
                //       errorMessage += "\nError: in " + str; //optional
                return false;
            }
            bool isArg = false;
            j++;
            word = "";
            int brackets = 1;
            while (j < str.Length && brackets != 0) {
                if (str[j] == '(' && !isConst) brackets++;
                if (str[j] == ')' && !isConst) brackets--;
                if (str[j].ToString() == "'") isConst = !isConst;
                if (brackets != 0 && str[j] != ',' && !isConst) word += str[j];
                if (str[j] == ',' && !isConst) {
                    isArg = true;
                    word = cleaning(word);
                    if (word[0].ToString() == "'" && word[word.Length - 1].ToString() == "'") word = "";
                    else {
                        if (!checkPart(word)) {
                            //   errorMessage += "\nError: in " + str; //optional
                            return false;
                        }
                        word = "";
                    }
                }
                if (isConst) word += str[j];
                j++;
            }
            if (brackets != 0) {
                strongError = true;
                errorMessage = "\nError: missing ) in " + str;
                return false;
            }
            if (isArg) {
                word = cleaning(word);
                if (word == "") {
                    strongError = true;
                    errorMessage += "\nError: missing argument in " + str;
                    return false;
                }
                if (word[0].ToString() == "'" && word[word.Length - 1].ToString() == "'") return true;
                if (!checkPart(word)) {
                    //   errorMessage += "\nError: in " + str; //optional
                    return false;
                }
            }
            if (j < str.Length) {
                for (int i = j; i < str.Length; i++) {
                    if (str[i] != ' ' || str[i] != ')') {
                        strongError = true;
                        errorMessage += "\nError: expected ; in " + str;
                        return false;
                    }
                }
            }
            return true;
        }


        //<state>::=<assign> | <func> | <id>
        private bool checkState(string str) {
            if (strongError) return false;
            if (str == "") {
                strongError = true;
                errorMessage += "\nError message - missing statement";
                return false;
            }
            str = cleaning(str);
            if (checkAssign(str)) { return true; }
            if (checkId(str)) { return true; }
            if(checkFunc(str)) { return true; }

            strongError = true;
            errorMessage += "\nError message - incorrect statement after then: " +str;
            return false;
        }
        
        private bool checkAssign(string str) {
            string word = "";
            int j = 0;
            while (j < str.Length && str[j]!=':') {
                word += str[j];
                j++;
            }
            if (j>=str.Length) {
                return false;
            }
            else {
                if (str[j+1] != '=') {
                    strongError = true;
                    errorMessage = "\nError message - expected := but found " + str[j + 1] + " in " + str;
                    return false;
                }
                word = cleaning(word);
                if (!checkId(word)) {
                    strongError = true;
                    errorMessage = "\nError message - you can assign only to identifier but there is " + word + " in " + str;
                    return false;
                }
                word = "";
                for(int i=j+2; i<str.Length; i++) {
                    word += str[i];
                }
                word = cleaning(word);
                if (word[0].ToString() == "'" && word.Length == 1) {
                    strongError = true;
                    errorMessage += "\nError message - missing ' in " + word;
                    return false;
                }
                if (word[0].ToString() == "'" && word[word.Length - 1].ToString() == "'") return true;

                if(!checkPart(word)) {
                    errorMessage += "\nError message - in " + str;
                    return false;
                }
            }
            return true;
        } 
    }
}
