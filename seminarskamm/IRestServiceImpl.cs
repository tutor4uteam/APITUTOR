using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace seminarskamm
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestServiceImpl" in both code and config file together.
    [ServiceContract]
    public interface IRestServiceImpl
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Tutor")]

        List<Tutor> Tutor();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Student")]
        List<Student> Student();


        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Tutor/{id}")]
        List<Tutor> TutorID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Student/{id}")]
        List<Student> StudentID(string id);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "addTutor/{name}/{surname}/{mail}/{phone}/{password}/{postNumber}/{street}/{houseNo}")]
        List<Result> registerTutor(string name, string surname, string mail, string phone, string password, string postNumber, string street, string houseNo);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "addStudent/{name}/{surname}/{mail}/{password}/{postNumber}/{street}/{houseNo}/{phone}")]

        List<Result> registerStudent(string name, string surname, string mail, string password, string postNumber, string street, string houseNo, string phone);

        //login tutor
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "LoginTutor/{username}/{password}")]

        List<ResultLoginTutor> loginTutor(string username, string password);

        //login student
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "LoginStudent/{username}/{password}")]

        List<ResultLoginStudent> loginStudent(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Subject")]

        List<Subject> subjectList();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Town")]

        List<Town> townList();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Town/{id}")]

        List<Town> townByID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Termin")]

        List<Termin> Termin();

        [OperationContract]
        [WebInvoke(Method = "GET",
         ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "TerminBySubjectId/{idSubject}")]

        List<Termin> TerminBySubjectId(string idSubject);

        
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TerminInfoById/{idTermin}")]
        List<Termin> terminInfoById(string idTermin);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DeleteTermin/{idTermin}")]
        List<Result> deleteTermin(string idTermin);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TerminInfoBySubjectId/{idSubject}")]
        List<Termin> terminInfoBySubjectId(string idSubject);


        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "addTermin/{idTutor}/{idSubject}/{date}")]

        List<Result> addTermin(string idTutor, string idSubject, string date);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Grade/{id}")]

        List<ResultDouble> getGradeByID(string id);


        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SubjectList")]

        List<Subject> allSubjects();

        //call id subject, get info tutor, and price
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Subject/{id}")]

        List<SubjectList> subjectID(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ChangePassTutor/{id}/{password}")]
        List<Result> changePassTutor(string id, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ChangePassStudent/{id}/{password}")]
        List<Result> changePassStudent(string id, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "MyTerminTutor/{id}")]
        List<TerminTutor> myTerminTutor(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "MyTerminTutorReserved/{id}")]
        List<TerminTutorReserved> myTerminTutorReserved(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "MyTerminStudent/{id}")]
        List<TerminStudent> myTerminStudent(string id);



        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "TakeTermin/{idTermin}/{idStudent}")]
        List<Result> takeTermin(string idTermin, string idStudent);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GiveGrade/{idTermin}/{grade}")]
        List<Result> giveGrade(string idTermin, string grade);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "addSubjectPrice/{idTutor}/{idSubject}/{price}")]
        List<Result> addSubjectPrice(string idTutor, string idSubject, string price);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "subjectITeach/{idTutor}")]
        List<Subject> subjectITeach(string idTutor);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "TerminByTutorAndSubject/{idTutor}/{idSubject}")]
        List<Termin> terminByTutorAndSubject(string idTutor, string idSubject);
        /*

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "TerminByTutor/{idTutor}")]
        List<Termin> terminByTutor(string idTutor, string idSubject);
        */


    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public int result { get; set; }

    }
    public class ResultLoginStudent
    {
        [DataMember]
        public int result { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string lastname { get; set; }
        [DataMember]
        public string mail { get; set; }
        [DataMember]
        public string street { get; set; }
        [DataMember]
        public int houseNumber { get; set; }
        [DataMember]
        public int postNumber { get; set; }
        [DataMember]
        public string postName { get; set; }
        [DataMember]
        public string phone { get; set; }



    }
    [DataContract]
    public class ResultLoginTutor
    {
        [DataMember]
        public int result { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string lastname{ get; set; }
        [DataMember]
        public string mail { get; set; }
        [DataMember]
        public string street { get; set; }
        [DataMember]
        public int houseNumber { get; set; }
        [DataMember]
        public int postNumber { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string postName { get; set; }
        [DataMember]
        public int grade { get; set; }
    }

    public class ResultDouble
    {
        [DataMember]
        public int grade { get; set; }
    }

    [DataContract]
    public class Login
    {
        [DataMember]
        public int result { get; set; }
        [DataMember]
        public int admin { get; set; }

    }
   





    public class Tutor
    {
        [DataMember]
        public int idTutor { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public string mail { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public double grade { get; set; }
    }

    public class Student
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public string mail { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string phone { get; set; }
    }

    public class Subject
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name {get; set;}
        [DataMember]
        public int freeTermin { get; set; }
        [DataMember]
        public int price { get; set; }
    }

    public class Town
    {
        [DataMember]
        public int postNumber { get; set; }
        [DataMember]
        public string name { get; set; }


    }

    public class Termin
    {
        [DataMember]
        public int idTermin { get; set; }
        [DataMember]
        public int idTutor { get; set; }
        [DataMember]
        public int idStudent { get; set; }
        [DataMember]
        public int idSubject { get; set; }
        [DataMember]
        public bool reserved { get; set; }
        [DataMember]
        public int grade { get; set; }
        [DataMember]
        public string tutorName { get; set; }
        [DataMember]
        public string tutorLastname{ get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string subject { get; set; }
        [DataMember]
        public int price { get; set; }



    }

    public class SubjectList
    {
        [DataMember]
        public int id{ get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public string adress { get; set; }
        [DataMember]
        public string mail { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public int price{ get; set; }
    }

    public class TerminTutorReserved
    {
        [DataMember]
        public int idTermin { get; set; }
        [DataMember]
        public string studentName { get; set; }
        [DataMember]
        public string studentLastname { get; set; }
        [DataMember]
        public int studentID { get; set; }
        [DataMember]
        public string subject { get; set; }
        [DataMember]
        public string date { get; set; }



    }

    public class TerminTutor
    {
        [DataMember]
        public int idTermin { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string subject { get; set; }
        [DataMember]
        public int price { get; set; }

    }


    public class TerminStudent
    {
        [DataMember]
        public int idTermin { get; set; }
        [DataMember]
        public string tutorName { get; set; }
        [DataMember]
        public string tutorLastname { get; set; }
        [DataMember]
        public int tutorId { get; set; }
        [DataMember]
        public string subject { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string date { get; set; }

    }
}
