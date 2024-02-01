using Stage03Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage03Library.Interface
{
    public interface IDatabaseRepository
    {
        //User Login,Registration and recover password
        void UserRegister(UserModel user);
        bool LoginUser(string email, string password);
        bool ChangePassword(string userName, int securityCode, string securityAnswer, string newPassword);
        List<SecurityQuestionTable> GetAllSecurityQuestions();
        //CRUD
        List<Person> GetAllIndividuals();
        Person GetIndividualbyId(long id);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(long id);
        int GetUserIdByEmail(string email);
    }
}
