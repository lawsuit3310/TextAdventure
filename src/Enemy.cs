﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.src
{
    class Enemy : Cursor
    {
        public int HP { get; set; }
        public int moveChance { get; set; }
        public static string[] Image = {
                "@@@@@@@@@@@@@@@@@@@@@*.@@@@@@@@@@@@@@@@@",
                "@@@@@@@@@@@@@@@@@./   (.*   (@@@@@@@@@@@",
                "@@@@@@@@@@@@@@@*.    (*(*/    @@@@@@@@@@",
                "@@@@@@@@@@@@@@@.* /*(((/((/**  ,@@@@@@@@",
                "@@@@@@@@@@@@@@..*/. %& . &%&,(* @@@@@@@@",
                "@@@@@@@@@@@@@@.**(,..       /%* @@@@@@@@",
                "@@@@@@@@@@@@@@,.**./(.    ###(* @@@@@@@@",
                "@@@@@@@@@@@@@@@@.,../#&&&@%%(#@@@@@@@@@@",
                "@@@@@@@@@@@@@@@.* /*,,#(/,  * @@@@@@@@@@",
                "@@@@@@@@@@@@/.      (//,/(*    @@@@@@@@@",
                "@@@@@@@@@@../*****((.,%#%(//(** @@@@@@@@",
                "@@@@@@@@.///(****/((////////#(/*(@@@@@@@",
                "@@@@@@@,.*//(***(//////////(///**/@@@@@@",
                "@@@@@@@,.***%%%/////////////*///***@@@@@",
                "@@@@@@@..#***@%%%%/////////#&*//****@@@@",
                "@@@@@@..*(**//(*%/.********,*%(//****&@@",
                "@@@@@.** /*(/#*,.,.,..,..,....%%%%*/**#@",
                "@@@...** /*(/(..,..,..,........%#/**.**%",
                "@@.*.,* ***((.. ... ......(.. . %#**(,**",
                ".....*(*/*******&&&&&&**&&&&&%**&%***.**",
                "@.,...**********//////**(/////*******.*%",
                "@@.*..**********/////(***////,*******@@@",
                "@@@.,.**********%%( /(***#*#/%*******@@@",
                "@@@@  **********%%(%%/***%#**%******@@@@",
                "@@@@@ .********.%%(%%****#&%%%****@@@@@@",
                "@@@@, ******....%%//%*****%%(%(**@@@@@@@",
                "@@@@  *****.....%%%%%,****%%*%%@@@@@@@@@",
                "@@@@@ /**.......%%%%%.****%%%%%@@@@@@@@@",
                "@@@@@@@ ......../%/*%..,**#%%%%@@@@@@@@@",
                "@@@@@@@@@@ ......%/(%...,***%%@@@@@@@@@@",
                "@@@@@@@@@@@@@@ ,..*#%....**(%%@@@@@@@@@@",
                "@@@@@@@@@@@@@@@@  (%%....( *%%@@@@@@@@@@",
                "@@@@@@@@@@@@@@@@. %(&@ #@  *#%@@@@@@@@@@",
                "@@@@@@@@@@@@@@@@  %/%@@@@ #%#%@@@@@@@@@@",
                "@@@@@@@@@@@@@@@@  %%%%@@  %%%%@@@@@@@@@@"
            };
        public void Damaged(Cursor obj)
        {
            HP -= obj.Atk;
        }
        public Enemy()
        {
            HP = Health;
        }
        public Enemy(int Atk) : this()
        {
            this.Atk = Atk;
        }

      


        public void printStatus()
        {

            Console.WriteLine("적 현재 체력 : {0}/{1}",HP,Health);
        }
    }
}
