using Stage03Library.Data;
using Stage03Library.Interface;
using Stage03Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Repository
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly DefaultDbContext _context;
        public DatabaseRepository()
        {
            _context = new DefaultDbContext();
        }

        public DatabaseRepository(DefaultDbContext context)
        {
            _context = context;
        }

        //CRUD
        public void AddPerson(Person person)
        {
            _context.PersonContext.Add(person);
            _context.SaveChanges();
        }

        public void DeletePerson(long id)
        {
            var person = _context.PersonContext.Find(id);
            if (person != null)
            {
                _context.PersonContext.Remove(person);
                _context.SaveChanges();
            }
        }

        public List<Person> GetAllIndividuals()
        {
            return _context.PersonContext.ToList();
        }


        public Person GetIndividualbyId(long id)
        {
            return _context.PersonContext.Find(id);
        }

        public void UpdatePerson(Person person)
        {
            var existingPerson = _context.PersonContext.Find(person.Id);
            if (existingPerson != null)
            {
                existingPerson.Name = person.Name;
                existingPerson.Gender = person.Gender;
                existingPerson.DOB = person.DOB;
                existingPerson.CreatedOn = person.CreatedOn;
                existingPerson.UpdatedOn = person.UpdatedOn;
                existingPerson.CreatedBy = person.CreatedBy;
                existingPerson.UpdatedBy = person.UpdatedBy;

                _context.SaveChanges();
            }
        }
        // User login
        public bool ChangePassword(string userName, int securityCode, string securityAnswer, string newPassword)
        {
            var user = _context.UserModelContext.SingleOrDefault(u=>u.Email == userName && u.SecurityCode == securityCode && u.SecurityAnswer == securityAnswer);
            if(user != null)
            {
                user.Password = newPassword;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<SecurityQuestionTable> GetAllSecurityQuestions()
        {
            return _context.SecurityQuestionTableContext.ToList();
        }

        public bool LoginUser(string email, string password)
        {
            var result = _context.UserModelContext.SingleOrDefault(x=>x.Email == email && x.Password == password);
            return result != null;
        }

        public void UserRegister(UserModel user)
        {
            if(_context.UserModelContext.Any(x => x.Email == user.Email))
            {
                throw new InvalidOperationException("Email is already registered");
            }

            var newUser = new UserModel()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                SecurityCode = user.SecurityCode,
                SecurityAnswer = user.SecurityAnswer,
                IsActive = true,
            };

            _context.UserModelContext.Add(newUser);
            _context.SaveChanges();
        }

        public int GetUserIdByEmail(string email)
        {
            var user = _context.UserModelContext.FirstOrDefault(u => u.Email == email);
            return user?.Id ?? 0;
        }
    }
}
