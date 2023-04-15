using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemApp_4J.Models;

namespace TestSystemApp_4J.Classes
{
    public class DataAccessLayer
    {
        public void LoadDataToDb()
        {
            TestSystemContext context = new TestSystemContext();

            User user = new User() { Login = "login", Password = "12345" };

            List<Question> questions = new List<Question>()
            {
                new Question() { Text = "Question 1?", Answer = true},
                new Question() { Text = "Question 2?", Answer = true},
                new Question() { Text = "Question 3?", Answer = false},
                new Question() { Text = "Question 4?", Answer = true},
                new Question() { Text = "Question 5?", Answer = false}
            };

            context.User.Add(user);

            context.Question.AddRange(questions);

            context.SaveChanges();
        }
    }
}
