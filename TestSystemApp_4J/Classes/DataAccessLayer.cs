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

        public static List<ViewModelResultUser> GetViewModelResultUsers(int id = 1)
        {
            int idUser = id;

            TestSystemContext context = new TestSystemContext();

            List<ViewModelResultUser> viewModelResultUsers = new List<ViewModelResultUser>();

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

        public static bool SignUp(string login, string password)
        {
            User user = new User()
            {
                Login = login, Password = password
            };

            TestSystemContext context = new TestSystemContext();

            User tempUser = context.User.FirstOrDefault(x => x.Login == login);

            if (tempUser != null)
            {
                return false;
            }

            context.User.Add(user);
            context.SaveChanges();
            return true;
        }

        public static List<User> GetAllUsers()
        {
            TestSystemContext context = new TestSystemContext();

            List<User> users = context.User.AsNoTracking().ToList();

            return users;
        }

        public static List<Test> GetAllTests()
        {
            TestSystemContext context = new TestSystemContext();

            List<Test> tests = context.Test.AsNoTracking().ToList();

            return tests;
        }

        public static List<Question> GetQuestionsForTest(int idTest)
        {
            TestSystemContext context = new TestSystemContext();

            var questions = context.TestQuestion.
                Where(x => x.TestId == idTest).
                Select(x => x.Question).AsNoTracking().ToList();

            return questions;
        }
    }
}
