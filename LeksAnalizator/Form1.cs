﻿using System;
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

        private bool startParse = false;

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
        // if Length(S) > 15 + 1 + 10 then Error
        private void button2_Click(object sender, EventArgs e) {
            label8.Text = "";
            string str = textBox2.Text;
            string getWord;
           // for(int i=0; i<str.Length;i++) {
                getWord = "";
                int i = 0;
                if (Char.IsLetter(str[i])) {
                    while(str[i] != ' ') {
                        getWord += str[i];
                        i++;
                    }
                    switch (checkWord(getWord)) {
                        case 0: label8.Text = "Error. Should start with IF"; break;
                        case 1: ifStatement(str, i); break;
                        default: break;
                    }
                }

//            }
        }


        private int findStatement(string str, int i) {
            string word = "";
            int posStart = i;
            while (i < str.Length && str[i] != '<' && str[i] != '>' && str[i] != '=') {
                word += str[i];
                i++;
            }
            bool q = true; //ture = has no statement before delim
            for (int j=0; j<word.Length; j++) {
                if (word[j] != ' ') q = false;
            }
            word = "";
            posStart = i;
            if (!q) {
                switch (str[i]) {
                    case '<':
                        i++;
                        if (str[i] == '<') {
                            label8.Text = "Error. Can't find statement.";
                            i = str.Length;
                        }
                        break;
                    case '>':
                        i++;
                        if (str[i] == '>' || str[i] == '<') {
                            label8.Text = "Error. Can't find statement.";
                            i = str.Length;
                        }
                        break;
                    case '=':
                        i++;
                        if (str[i] == '>' || str[i] == '<' || str[i] == '=') {
                            label8.Text = "Error. Can't find statement.";
                            i = str.Length;
                        }
                        break;
                    default:
                        label8.Text = "Error. No statement.";
                        break;
                }
                if (label8.Text == "") {
                    while (i < str.Length && !word.Contains("then")) {
                        word += str[i];
                        i++;
                    }
                    if (i < str.Length) {
                        int n = word.IndexOf("then");
                        word = word.Remove(n, 4);
                    }
                    q = true;
                    for (int j = 0; j < word.Length; j++) {
                        if (word[j] != ' ') q = false;
                    }
                    if (!q) {
                        return posStart;
                    }
                    else return str.Length;
                }
                else return str.Length;
            }
            else return str.Length;
        }

        private void ifStatement(string str, int i) {
            //if (statement) then (do)
            //statement = numb/id </>/<=/>=/=/<> num/id
            i++; //next symbol after space
            string word = "";
            bool findEnd = false;
            if (str[i] == '(' || Char.IsLetter(str[i]) || Char.IsDigit(str[i]) || str[i] == ' ') {
                if (str[i] == '(') findEnd = true;
                i = findStatement(str, i);
                if (i != str.Length) {
                 //   i--;
                    word = "";
                    // ( was
                    if (findEnd) {
                        while (i < str.Length && str[i] != ')') i++;
                        if (i < str.Length && str[i] == ')') {
                            i++;
                            while (i < str.Length && word != "then"  ) {
                                if (str[i] == ' ') word = "";
                                else word += str[i];
                                i++;
                            }
                        }
                        else {
                            label8.Text = "Error. No statement.";
                        }
                    }
                    // ( wasnt
                    else {
                        while (i < str.Length && word != "then") {
                            if (str[i] == ' ') word = "";
                            else word += str[i];
                            i++;
                        }
                    }
                    //dont find "then"
                    if (i == str.Length - 1 && label8.Text == "") {
                        label8.Text = "Error. Don't find THEN.";
                    }
                    else {
                        if (i != str.Length && str[i] ==' ') {
                            label8.Text = "Success.";
                        }
                        else {
                            if(i>= str.Length-1 && label8.Text == "")  label8.Text = "Error. Should do something after THEN.";
                            else {
                                if (str[i] != ' ') label8.Text = "Error. Don't find THEN.";
                            }
                        }
                    }


                }
                else {
                    label8.Text = "Error. No statement.";
                }
            }
            else {
                label8.Text = "Error. No statement.";
            }
        }

        //0 -not keyword 1 - keyword 
        private int checkWord(string word) {
            if (word == "if") return 1;
            else return 0;
        }
    }
}
