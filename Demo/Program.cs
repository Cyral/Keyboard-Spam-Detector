using System;
using System.Collections.Generic;

namespace Cyral.KeyboardSpamDetector.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var detector = new SpamDetector();
            var tests = new Dictionary<string, bool>
            {
                {"Sdf sdfsdfds fsff Dds fsf", true},
                {"wererwwrwerweerer rw rewrwerwererwr", true},
                {"hwajt the fdu eh ui futugt tge ufgb ets hewat ti sdo it", true},
                {"yrt asre the bertpera sojifsi hwrifle seen fie sue usertaje rwewererwer", true},
                {"EiybvisgfdsffbiFALFIFOBIOFUIBFRBsjdkdkdsjf", true},
                {"igish ssddf sdf", true},
                {"ioacdu uqeidiqdudaisdu a s diudsdi dusu doidu aso diuodsi uoiduaudsiaudiudasd aud", true},
                {"zvuoiyvci zoioizuoicu i udaou oiuoiuiweuiou we uoqu urqu ieuiuioqouwu we uuiowu", true},
                {"wert yuos", true},
                {"uadfa ffo sod fhfofdos fa jisjd efdqw wqddwdww", true},
                {"qrqrqr eri erer yqe ryry dfqeroworyqrqer rrqrere wrqo", true},
                {
                    ".snoitcurtsni eht yb deificeps snoitarepo )O/I( tuptuo/tupni dna lortnoc ,lacigol ,citemhtira cisab eht gnimrofrep yb margorp retupmoc a fo snoitcurtsni eht tuo seirrac taht retupmoc a nihtiw yrtiucric cinortcele eht si )UPC( tinu gnissecorp lartnec A",
                    true
                },
                {"easd ojd eqorj daiweo jowfie qjo qwej jo w weji", true},
                {"nzcna di sidadoitna ffeha ef eq h hqq fqh erhe rr qrhre afe oqiwhi qweihuho f", true},
                {"rtesting sdf what sdf re foosdf", true},
                {"This is an example", false},
                {"Yes", false},
                {"for loop", false},
                {"Awesome", false},
                {"dictionary", false},
                {"apple bannana orange", false},
                {"Ok thanks", false},
                {
                    "A central processing unit (CPU) is the electronic circuitry within a computer that carries out the instructions of a computer program by performing the basic arithmetic, logical, control and input/output (I/O) operations specified by the instructions.",
                    false
                },
                {"This library detects if a phrase is spam or a legitimate sentence", false},
                {"Computer programming, often shortened to programming, sometimes called coding", false},
                {
                    "In cryptography, encryption is the process of encoding messages or information in such a way that only authorized parties can read it.",
                    false
                },
                {"keyboard spam detector", false},
            };

            foreach (var test in tests)
            {
                var result = detector.IsSpam(test.Key);
                Console.ForegroundColor = ConsoleColor.Green;
                if (result != test.Value)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(result.ToString().ToUpper());
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(" " + test.Key);
            }
            Console.WriteLine();

            while (true)
            {
                var phrase = Console.ReadLine();
                var result = detector.IsSpam(phrase);
                Console.ForegroundColor = ConsoleColor.Green;
                if (result)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result ? "Spam" : "Not Spam");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}