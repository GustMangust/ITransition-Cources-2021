using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Task3 {
  class Program {
    public static string GenerateRandomString(int length) {
      string allowableChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      var allowable = allowableChars.ToCharArray();
      var l = allowable.Length;
      var chars = new char[length];
      for(var i = 0; i < length; i++) {
        chars[i] = allowable[RandomNumberGenerator.GetInt32(0,l)];
      }
      return new string(chars);
    }
    public static string Menu() { 
      Console.WriteLine("Available moves:");
      Console.WriteLine("1 - a\n2 - b\n3 - c\n4 - d\n5 - e\n6 - f\n7 - g\n0 - exit");
      Console.WriteLine("Enter your move:");
      try {
        int choice = Convert.ToInt32( Console.ReadLine());
        if(choice > 7 || choice < 0) {
          throw new Exception();
        }
        if(choice == 0) {
          Environment.Exit(1);
        }
        return moves[choice - 1];
      }
      catch {
        Console.WriteLine("Enter the correct line");
        return Menu();
      }
    }
    public static bool CheckStrings(string[] words) {
      foreach(var a in words) {
        if(!moves.Contains(a)) {
          return false;
        }
      }
      return true;
    }
    static string[] moves = { "a", "b", "c", "d", "e","f","g" };
    public static void Result(string userChoice,string compChoice) {
      List<string> movesList = new List<string>(moves);
      int userIndex = movesList.IndexOf(userChoice);
      int compIndex = movesList.IndexOf(compChoice);
      int rest = moves.Length / 2;
      if(userIndex == compIndex) {
        Console.WriteLine("No winner");
        return;
      }
      if(userIndex+rest >= moves.Length) {
        if(compIndex>userIndex && compIndex < userIndex + rest) {
          Console.WriteLine("You lose");
          return;
        }
        rest = rest - (moves.Length - 1 - userIndex);
        if(compIndex>=0 && compIndex < rest) {
          Console.WriteLine("You lose");
          return;
        }
      } else {
        if(compIndex > userIndex && compIndex < userIndex + rest) {
          Console.WriteLine("You lose");
          return;
        }
      }
      Console.WriteLine("You win");
    }
    static void Main(string[] args) {
      if(!args.GroupBy(x => x).Any(x => x.Count() > 1)&& args.Length>=3 && args.Length%2!=0 && CheckStrings(args)) {
        for(int i = 0; i < args.Length; i++) {
          string key = GenerateRandomString(128);
          using(var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key))) {
            var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(args[i]));
            Console.WriteLine("HMAC:\n" + Convert.ToBase64String(hash));
            string userChoice = Menu();
            Console.WriteLine("Your move: " + userChoice);
            Result(userChoice, args[i]);
            Console.WriteLine("HMAC key: "+ key);
          }
        }
        }else {
          Console.WriteLine("Input string like this: a b c d e.\nAmount of strings must be odd and more than 3.\nNo reapiting strings");
        }
    }
  }
}
