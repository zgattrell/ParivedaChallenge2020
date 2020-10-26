using System;
using System.Collections.Generic;

namespace parivedaCompetition
{
    public class Function
    {

        private string name;
        private string[] args;
        private List<String> body;

        public Function(string name, string[] args){
            this.name = name;
            this.args = new string[args.Length];
            for(int i = 0; i < args.Length; i++){
                this.args[i] = args[i];
            }
        }
        
        public string getName(){
            return name;
        }
        public String[] getArgs(){
            return args;
        }

        public List<String> getBody(){
            return body;
        }

        public void setBody(List<String> body){
            this.body = body;
        }



    }
}