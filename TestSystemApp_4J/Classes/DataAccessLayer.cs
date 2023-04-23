using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemApp_4J.Models;
using TestSystemApp_4J.Models.ViewModels;

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

        public static List<ViewModelResultUser> GetViewModelResultUsers()
        {
            TestSystemContext context = new TestSystemContext();

            List<ViewModelResultUser> viewModelResultUsers = new List<ViewModelResultUser>();

            int idUser = 2;

            //var user = GetUsers().FirstOrDefault(x => x.Id == idUser);
            //var results = GetResults().Where(x => x.UserId == idUser).ToList();

            List<Result> results = context.User.Include(x => x.Results).
                FirstOrDefault(x => x.Id == idUser).Results.ToList();

            int num = 1;
            foreach (var result in results)
            {
                ViewModelResultUser vm = new ViewModelResultUser()
                {
                    Id = num,
                    UserName = result.User.Login,
                    Score = result.Scores,
                    MaxScores = result.MaxScores,
                    StartDate = result.StartDateTime.ToShortDateString(),
                    EndDate = result.EndDateTime.ToShortDateString(),
                    StartTime = result.StartDateTime.ToShortTimeString(),
                    EndTime = result.EndDateTime.ToShortTimeString(),
                };
                num++;

                viewModelResultUsers.Add(vm);
            }

            return viewModelResultUsers;
        }


        public static User SignIn(string login, string password)
        {
            TestSystemContext context = new TestSystemContext();

            User user = context.User.FirstOrDefault(x => x.Login == login && x.Password == password);

            return user;
        }
    }
}
