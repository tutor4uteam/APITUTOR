using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;

namespace seminarskamm
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestServiceImpl" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestServiceImpl.svc or RestServiceImpl.svc.cs at the Solution Explorer and start debugging.
    public class RestServiceImpl : IRestServiceImpl
    {
        string connectionStrings = ConfigurationManager.ConnectionStrings["tutorConnectionString"].ConnectionString;

        //md5 hash
        static string toMD5(MD5 md5Hash, string vnesenoGeslo)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(vnesenoGeslo));

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                strBuilder.Append(data[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        //chechk if tutor exist
        public bool checkMailTutor(string mail)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getUser = new SqlCommand("SELECT COUNT(*) FROM tutor where mail = '" + mail + "'", connection))
                {
                    connection.Open();
                    object data = getUser.ExecuteScalar();
                    connection.Close();
                    if (data != null)
                    {
                        result = Int32.Parse(data.ToString());
                        if (result > 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //check if user exist

        public bool checkMailStudent(string mail)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getUser = new SqlCommand("SELECT COUNT(*) FROM student where mail = '" + mail + "'", connection))
                {
                    connection.Open();
                    object data = getUser.ExecuteScalar();
                    connection.Close();
                    if (data != null)
                    {
                        result = Int32.Parse(data.ToString());
                        if (result > 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //return all tutors
        public List<Tutor> Tutor()
        {
            List<Tutor> list = new List<Tutor>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {

                using (SqlCommand getData = new SqlCommand("select t.id_tutor, t.name, t.lastname, t.mail, t.phone, t.street, t.house_no, c.name, AVG(CASE WHEN ter.grade <> 0 THEN ter.grade ELSE null END) as grade " +
                    "FROM tutor t left join termin ter on ter.id_tutor = t.id_tutor inner join town c ON c.post_number = t.post_number " +
                    "GROUP BY t.id_tutor, t.name, t.lastname, t.mail, t.phone, t.street, t.house_no, c.name; ", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            if (myReader.IsDBNull(8))
                            {
                                list.Add(new Tutor
                                {
                                    idTutor = myReader.GetInt32(0),
                                    name = myReader.GetString(1),
                                    surname = myReader.GetString(2),

                                    mail = myReader.GetString(3),
                                    phone = myReader.GetString(4),
                                    address = myReader.GetString(5) + " " + myReader.GetInt32(6) + ", " + myReader.GetString(7),
                                    grade = 0

                                });
                            }
                            else
                            {
                                list.Add(new Tutor
                                {
                                    idTutor = myReader.GetInt32(0),
                                    name = myReader.GetString(1),
                                    surname = myReader.GetString(2),

                                    mail = myReader.GetString(3),
                                    phone = myReader.GetString(4),
                                    address = myReader.GetString(5) + " " + myReader.GetInt32(6) + ", " + myReader.GetString(7),
                                    grade = myReader.GetInt32(8)

                                });
                            }


                        }
                        catch (Exception e)
                        {
                            list.Add(new Tutor
                            {
                                name = "false",
                                surname = "false",
                                mail = "false",
                                phone = "false",
                                address = "false"
                            });


                        }
                    }
                    connection.Close();
                }
            }
            return list;
        }

        //return list of all students
        public List<Student> Student()
        {
            List<Student> list = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {

                using (SqlCommand getData = new SqlCommand("SELECT s.name, s.lastname, s.mail, s.street, s.house_no,t.name, s.phone from student s, town t where s.post_number = t.post_number", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Student
                            {

                                name = myReader.GetString(0),
                                surname = myReader.GetString(1),
                                mail = myReader.GetString(2),
                                address = myReader.GetString(3) + " " + myReader.GetInt32(4) + ", " + myReader.GetString(5),
                                phone =myReader.GetString(6)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Student
                            {
                                name = "false",
                                surname = "false",
                                mail = "false",
                                address = "false",
                                phone = "false"

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        //login and return all data of student
        public List<ResultLoginStudent> loginStudent(string username, string password)
        {
            List<ResultLoginStudent> retVal = new List<ResultLoginStudent>();

            int num = 0;
            MD5 md5Hash = MD5.Create();

            using (SqlConnection con = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT COUNT(*) FROM student WHERE mail = '" + username + "' AND password = '" + toMD5(md5Hash, password) + "'", con))
                {
                    con.Open();
                    object data = getData.ExecuteScalar();
                    con.Close();


                    if (data != null)
                    {
                        num = Int32.Parse(data.ToString());
                        if (num > 0)
                        {
                            using (SqlCommand getStudentData = new SqlCommand("SELECT s.id_student, s.name, s.lastname, s.mail, s.street, s.house_no, s.post_number, t.name, s.phone FROM student s, town t where mail='" + username + "' AND s.post_number = t.post_number;", con))
                            {
                                con.Open();
                                SqlDataReader myReader = getStudentData.ExecuteReader();
                                while (myReader.Read())
                                {
                                    try
                                    {
                                        retVal.Add(new ResultLoginStudent
                                        {
                                            id = myReader.GetInt32(0),
                                            name = myReader.GetString(1),
                                            lastname = myReader.GetString(2),
                                            mail = myReader.GetString(3),
                                            street = myReader.GetString(4),
                                            houseNumber = myReader.GetInt32(5),
                                            postNumber = myReader.GetInt32(6),
                                            postName = myReader.GetString(7),
                                            phone = myReader.GetString(8),
                                            result = 1
                                        }
                                        );
                                    }
                                    catch (Exception e)
                                    {
                                        retVal.Add(new ResultLoginStudent
                                        {
                                            name = e.Message,
                                            lastname = "false",
                                            mail = "false",
                                            street = "false",
                                            houseNumber = 0,
                                            postNumber = 0,
                                            result = -1

                                        });
                                    }

                                }
                                con.Close();
                            }
                            //retVal.Add(new ResultLoginStudent { result = 1 });
                            return retVal;
                        }
                    }
                }
                using (SqlCommand getData = new SqlCommand("SELECT COUNT(*) FROM student WHERE mail = '" + username + "' AND password = '" + toMD5(md5Hash, password) + "'", con))
                {
                    con.Open();
                    object data = getData.ExecuteScalar();
                    con.Close();

                    if (data != null)
                    {
                        num = Int32.Parse(data.ToString());
                        if (num > 0)
                        {
                            retVal.Add(new ResultLoginStudent { result = 0 });
                            return retVal;
                        }
                    }
                }

            }
            retVal.Add(new ResultLoginStudent { result = -1 });

            return retVal;
        }

        //login and return all data of tutor
        public List<ResultLoginTutor> loginTutor(string username, string password)
        {
            List<ResultLoginTutor> retVal = new List<ResultLoginTutor>();

            int num = 0;
            MD5 md5Hash = MD5.Create();

            using (SqlConnection con = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT COUNT(*) FROM tutor WHERE mail = '" + username + "' AND password = '" + toMD5(md5Hash, password) + "'", con))
                {
                    con.Open();
                    object data = getData.ExecuteScalar();
                    con.Close();


                    if (data != null)
                    {
                        num = Int32.Parse(data.ToString());
                        if (num > 0)
                        {
                            // z oceno SELECT t.id_tutor, t.name, t.lastname, t.mail, t.street, t.house_no, t.post_number, t.phone, p.name, avg(ter.grade) FROM tutor t, town p, termin ter where mail='miha@mail.com' AND p.post_number = t.post_number and ter.id_tutor = t.id_tutor AND ter.grade != 0  GROUP BY t.id_tutor, t.name, t.lastname, t.mail, t.street, t.house_no, t.post_number, t.phone, p.name;
                            //brez ocene  SELECT t.id_tutor, t.name, t.lastname, t.mail, t.street, t.house_no, t.post_number, t.phone, p.name FROM tutor t, town p where mail='" + username + "' AND p.post_number = t.post_number;"
                            using (SqlCommand getTutorData = new SqlCommand("SELECT t.id_tutor, t.name, t.lastname, t.mail, t.street, t.house_no, t.post_number, t.phone, p.name FROM tutor t, town p where mail='" + username + "' AND p.post_number = t.post_number;", con))
                            {
                                con.Open();
                                SqlDataReader myReader = getTutorData.ExecuteReader();
                                while (myReader.Read())
                                {
                                    try
                                    {
                                        retVal.Add(new ResultLoginTutor
                                        {
                                            id = myReader.GetInt32(0),
                                            name = myReader.GetString(1),
                                            lastname = myReader.GetString(2),
                                            mail = myReader.GetString(3),
                                            street = myReader.GetString(4),
                                            houseNumber = myReader.GetInt32(5),
                                            postNumber = myReader.GetInt32(6),
                                            phone = myReader.GetString(7),
                                            postName = myReader.GetString(8),
                                            result = 1
                                        }
                                        );
                                    }
                                    catch (Exception e)
                                    {
                                        retVal.Add(new ResultLoginTutor
                                        {
                                            name = e.Message,
                                            lastname = "false",
                                            mail = "false",
                                            street = "false",
                                            houseNumber = 0,
                                            postNumber = 0,
                                            phone = "false",
                                            result = -1

                                        });
                                    }

                                }
                                con.Close();
                            }
                            return retVal;
                        }
                    }
                }
                using (SqlCommand getData = new SqlCommand("SELECT COUNT(*) FROM tutor WHERE mail = '" + username + "' AND password = '" + toMD5(md5Hash, password) + "'", con))
                {
                    con.Open();
                    object data = getData.ExecuteScalar();
                    con.Close();

                    if (data != null)
                    {
                        num = Int32.Parse(data.ToString());
                        if (num > 0)
                        {
                            retVal.Add(new ResultLoginTutor { result = 0 });
                            return retVal;
                        }
                    }
                }

            }


            retVal.Add(new ResultLoginTutor { result = -1 });

            return retVal;
        }

        //register for student
        public List<Result> registerStudent(string name, string surname, string mail, string password, string postNumber, string street, string houseNo, string phone)
        {
            MD5 md5Hash = MD5.Create();
            var retVal = new List<Result>();
            int res;
            if (checkMailStudent(mail))
            {
                using (SqlConnection con = new SqlConnection(connectionStrings))
                {
                    con.Open();

                    string query1 = "INSERT INTO student(name, lastname, mail, password, street, house_no,post_number, phone) VALUES (@name, @lastname, @mail, @password, @street, @house_no, @post_number, @phone)";
                    SqlCommand com1 = new SqlCommand(query1, con);
                    com1.Parameters.AddWithValue("@name", name);
                    com1.Parameters.AddWithValue("@lastname", surname);
                    com1.Parameters.AddWithValue("@mail", mail);
                    com1.Parameters.AddWithValue("@password", toMD5(md5Hash, password));
                    com1.Parameters.AddWithValue("@street", street);
                    com1.Parameters.AddWithValue("@house_no", Int32.Parse(houseNo));
                    com1.Parameters.AddWithValue("@post_number", Int32.Parse(postNumber));
                    com1.Parameters.AddWithValue("@phone", phone);

                    res = com1.ExecuteNonQuery();

                    con.Close();
                }
                retVal.Add(new Result { result = res });
                return retVal;
            }
            else
            {
                retVal.Add(new Result { result = -1 });
                return retVal;
            }

        }

        //register for tutor
        public List<Result> registerTutor(string name, string surname, string mail, string phone, string password, string postNumber, string street, string houseNo)
        {
            MD5 md5Hash = MD5.Create();
            var retVal = new List<Result>();
            int res;


            if (checkMailTutor(mail))
            {
                using (SqlConnection con = new SqlConnection(connectionStrings))
                {
                    con.Open();

                    string query1 = "INSERT INTO tutor(name, lastname, mail, password, phone, street, house_no,post_number) VALUES (@name, @lastname, @mail, @password, @phone, @street, @house_no, @post_number)";
                    SqlCommand com1 = new SqlCommand(query1, con);
                    com1.Parameters.AddWithValue("@name", name);
                    com1.Parameters.AddWithValue("@lastname", surname);
                    com1.Parameters.AddWithValue("@mail", mail);
                    com1.Parameters.AddWithValue("@password", toMD5(md5Hash, password));
                    com1.Parameters.AddWithValue("@phone", phone);
                    com1.Parameters.AddWithValue("@street", street);
                    com1.Parameters.AddWithValue("@house_no", Int32.Parse(houseNo));
                    com1.Parameters.AddWithValue("@post_number", Int32.Parse(postNumber));

                    res = com1.ExecuteNonQuery();

                    con.Close();
                }
                retVal.Add(new Result { result = res });
                return retVal;
            }
            else
            {
                retVal.Add(new Result { result = -1 });
                return retVal;
            }


        }

        //retutn all subject and number of free termins
        public List<Subject> subjectList()
        {
            List<Subject> list = new List<Subject>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT b.id_subject, b.subject, COUNT(a.id_termin) FROM termin a, subject b WHERE a.id_subject = b.id_subject AND a.reserved = 0 GROUP BY b.id_subject, b.subject", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Subject
                            {
                                id = myReader.GetInt32(0),
                                name = myReader.GetString(1),
                                freeTermin = myReader.GetInt32(2)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Subject
                            {
                                id = 999,
                                name = "false"

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Tutor> TutorID(string id)
        {
            List<Tutor> list = new List<Tutor>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                //SELECT s.name, s.lastname, s.mail, s.street, s.house_no, t.name from student s, town t where s.post_number = t.post_number
                using (SqlCommand getData = new SqlCommand("SELECT t.name, t.lastname, t.mail, t.phone, t.street, t.house_no, p.name from tutor t, town p where t.post_number = p.post_number and t.id_tutor=@idtutor", connection))
                {
                    getData.Parameters.AddWithValue("idtutor", Int32.Parse(id));
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Tutor
                            {

                                name = myReader.GetString(0),
                                surname = myReader.GetString(1),
                                mail = myReader.GetString(2),
                                phone = myReader.GetString(3),
                                address = myReader.GetString(4) + " " + myReader.GetInt32(5) + ", " + myReader.GetString(6)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Tutor
                            {
                                name = "false",
                                surname = "false",
                                mail = "false",
                                phone = "false",
                                address = "false"
                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Town> townList()
        {
            List<Town> list = new List<Town>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT post_number, name FROM town", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Town
                            {
                                postNumber = myReader.GetInt32(0),
                                name = myReader.GetString(1)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Town
                            {
                                postNumber = 999,
                                name = "false"

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Town> townByID(string id)
        {
            List<Town> list = new List<Town>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {

                using (SqlCommand getData = new SqlCommand("SELECT name FROM town where post_number = @postNumber", connection))
                {
                    getData.Parameters.AddWithValue("postNumber", Int32.Parse(id));
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Town
                            {

                                name = myReader.GetString(0),

                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Town
                            {
                                name = "false",

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<ResultDouble> getGradeByID(string id)
        {
            int num;
            var retVal = new List<ResultDouble>();
            using (SqlConnection con = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT AVG(CASE WHEN grade <> 0 THEN grade ELSE null END) AS grade FROM termin WHERE id_tutor =" + Int32.Parse(id) +";", con))
                {
                    con.Open();
                    object data = getData.ExecuteScalar();
                    con.Close();
                    if (data != null)
                    {
                        try
                        {
                            num = Int32.Parse(data.ToString());
                            retVal.Add(new ResultDouble { grade = num });
                        }
                        catch (Exception e)
                        {
                            retVal.Add(new ResultDouble { grade = 0 });
                        }
                    }
                }
                return retVal;
            }
        }

        public List<Student> StudentID(string id)
        {
            List<Student> list = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                //SELECT s.name, s.lastname, s.mail, s.street, s.house_no, t.name from student s, town t where s.post_number = t.post_number
                using (SqlCommand getData = new SqlCommand("SELECT s.name, s.lastname, s.mail, s.street, s.house_no, t.name from student s, town t where t.post_number = s.post_number and s.id_student=@idstudent", connection))
                {
                    getData.Parameters.AddWithValue("idstudent", Int32.Parse(id));
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Student
                            {

                                name = myReader.GetString(0),
                                surname = myReader.GetString(1),
                                mail = myReader.GetString(2),
                                address = myReader.GetString(3) + " " + myReader.GetInt32(4) + ", " + myReader.GetString(5)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Student
                            {
                                name = "false",
                                surname = "false",
                                mail = "false",
                                address = "false"
                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<SubjectList> subjectID(string id)
        {
            List<SubjectList> list = new List<SubjectList>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT t.id_tutor, t.name, t.lastname, t.phone, t.mail, t.street, t.house_no, p.name, s.price FROM tutor t, town p, subject_tutor s WHERE t.post_number = p.post_number AND s.id_tutor = t.id_tutor AND s.id_subject= '"+id+"';", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new SubjectList
                            {
                                id=myReader.GetInt32(0),
                                name = myReader.GetString(1),
                                surname = myReader.GetString(2),
                                phone = myReader.GetString(3),
                                mail = myReader.GetString(4),                               
                                adress = myReader.GetString(5) +" "+myReader.GetInt32(6)+", "+myReader.GetString(7),
                                price = myReader.GetInt32(8) 
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new SubjectList
                            {
                                id = -1,
                                name = "false",
                                surname = "false",
                                phone = "false",
                                mail = "false",
                                adress = "false",
                                price = -1
                               
                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Result> changePassTutor(string id, string password)
        {
            MD5 md5Hash = MD5.Create();
            var retVal = new List<Result>();
            int res;
            using (SqlConnection con = new SqlConnection(connectionStrings))
            {
                con.Open();

                string query = "UPDATE tutor SET password = @newpassword WHERE id_tutor = @idtutor";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@newpassword", toMD5(md5Hash,password));
                com.Parameters.AddWithValue("@idtutor", Int32.Parse(id));
                com.ExecuteNonQuery();
                res = com.ExecuteNonQuery();
                con.Close();
            }
            retVal.Add(new Result { result = res });
            return retVal;
        }

        public List<Result> changePassStudent(string id, string password)
        {
            MD5 md5Hash = MD5.Create();
            var retVal = new List<Result>();
            int res;
            using (SqlConnection con = new SqlConnection(connectionStrings))
            {
                con.Open();

                string query = "UPDATE student SET password = @newpassword WHERE id_student = @idstudent";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@newpassword", toMD5(md5Hash, password));
                com.Parameters.AddWithValue("@idstudent", Int32.Parse(id));
                com.ExecuteNonQuery();
                res = com.ExecuteNonQuery();
                con.Close();
            }
            retVal.Add(new Result { result = res });
            return retVal;
        }

        public List<Termin> Termin()
        {
            List<Termin> list = new List<Termin>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {

                using (SqlCommand getData = new SqlCommand("SELECT a.id_termin, b.id_tutor, b.name, b.lastname, a.date, c.subject, d.price FROM termin a, tutor b, subject c, subject_tutor d WHERE a.reserved = 0 AND a.id_tutor = b.id_tutor AND c.id_subject = a.id_subject AND d.id_subject = a.id_subject and d.id_tutor = a.id_tutor", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Termin
                            {
                                idTermin = myReader.GetInt32(0),
                                idTutor = myReader.GetInt32(1),
                                tutorName = myReader.GetString(2),
                                tutorLastname = myReader.GetString(3),
                                date = myReader.GetString(4),
                                subject = myReader.GetString(5),
                                price = myReader.GetInt32(6)

                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Termin
                            {
                                idTermin = -1,
                                subject = e.Message

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;

        }

        //fuka oddajanje datime dormata v URLju
        public List<Result> addTermin(string idTutor,string idSubject, string date)
        {
            var retVal = new List<Result>();
            int res;
            /*
            try
            {
            */
                using (SqlConnection con = new SqlConnection(connectionStrings))
                {
                    con.Open();

                    string query1 = "INSERT INTO termin(id_tutor, id_subject,reserved,date, grade) VALUES (@idtutor, @idsubject,@reserved,@date, @grade)";
                    SqlCommand com1 = new SqlCommand(query1, con);
                    com1.Parameters.AddWithValue("@idtutor", Int32.Parse(idTutor));
                    com1.Parameters.AddWithValue("@idsubject", Int32.Parse(idSubject));
                    com1.Parameters.AddWithValue("@reserved", "False");
                    com1.Parameters.AddWithValue("@date",date);
                    com1.Parameters.AddWithValue("@grade", 0);

                res = com1.ExecuteNonQuery();

                    con.Close();
                }
                retVal.Add(new Result { result = res });
                return retVal;
                /*
            }catch (Exception e)
            {
                retVal.Add(new Result { result = -1 });
                return retVal;
            }*/

        }
        //naredi na dva klica ker nemores vidit
        //naredi na zasedene in na ne zasedene
        //spodnja metoda prikazuje termine za nezasedene termine
        public List<TerminTutor> myTerminTutor(string id)
        {
            List<TerminTutor> list = new List<TerminTutor>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT a.id_termin, a.date, b.subject, c.price FROM termin a, subject b, subject_tutor c WHERE a.id_tutor = "+Int32.Parse(id)+ " AND b.id_subject = a.id_subject AND a.reserved = 0 AND c.id_tutor = "+Int32.Parse(id)+" AND c.id_subject = a.id_subject", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new TerminTutor
                            {
                                idTermin = myReader.GetInt32(0),
                                date = myReader.GetString(1),
                                subject = myReader.GetString(2),
                                price = myReader.GetInt32(3)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new TerminTutor
                            {
                                idTermin = -1,
                                subject="False"
                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<TerminStudent> myTerminStudent(string id)
        {
            List<TerminStudent> list = new List<TerminStudent>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT DISTINCT a.id_termin, b.id_tutor, b.name, b.lastname, b.street, b.house_no, c.name, d.subject, a.date from termin a, tutor b, town c, subject d WHERE a.id_tutor = b.id_tutor AND a.id_student = "+ Int32.Parse(id)+"  AND c.post_number = b.post_number AND a.id_subject = d.id_subject", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new TerminStudent
                            {
                                idTermin = myReader.GetInt32(0),
                                tutorId = myReader.GetInt32(1),
                                tutorName = myReader.GetString(2),
                                tutorLastname = myReader.GetString(3),
                                address = myReader.GetString(4) + myReader.GetInt32(5) + myReader.GetString(6),
                                subject = myReader.GetString(7),
                                date = myReader.GetString(8)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new TerminStudent
                            {
                                idTermin = 0,
                                tutorId = 0,
                                tutorName = "False",
                                tutorLastname = "False",
                                address = "False",
                                subject = "False"
                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<TerminTutorReserved> myTerminTutorReserved(string id)
        {
            List<TerminTutorReserved> list = new List<TerminTutorReserved>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT a.id_termin, b.id_student, b.name, b.lastname, c.subject, a.date FROM termin a, student b, subject c WHERE a.id_student = b.id_student AND c.id_subject = a.id_subject AND a.id_tutor = "+Int32.Parse(id)+" and a.reserved=1", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new TerminTutorReserved
                            {
                                idTermin = myReader.GetInt32(0),
                                studentID = myReader.GetInt32(1),
                                studentName = myReader.GetString(2),
                                studentLastname = myReader.GetString(3),
                                subject = myReader.GetString(4),
                                date = myReader.GetString(5)

                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new TerminTutorReserved
                            {
                                idTermin = -1,
                                subject = "False"
                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Result> takeTermin(string idTermin, string idStudent)
        {
            var retVal = new List<Result>();
            int res;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionStrings))
                {
                    con.Open();

                    string query = "UPDATE termin SET id_student = @idstudent, reserved = 1 WHERE id_termin = @idtermin";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@idstudent", Int32.Parse(idStudent));
                    com.Parameters.AddWithValue("@idtermin", Int32.Parse(idTermin));

                    com.ExecuteNonQuery();
                    res = com.ExecuteNonQuery();
                    con.Close();
                }
                retVal.Add(new Result { result = res });
                return retVal;
            }catch(Exception e)
            {
                retVal.Add(new Result { result = -1 });
                return retVal;
            }

        }

        public List<Result> giveGrade(string idTermin, string grade)
        {
            var retVal = new List<Result>();
            int res;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionStrings))
                {
                    con.Open();

                    string query = "UPDATE termin SET grade = @grade WHERE id_termin = @idtermin";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@grade", Int32.Parse(grade));
                    com.Parameters.AddWithValue("@idtermin", Int32.Parse(idTermin));

                    com.ExecuteNonQuery();
                    res = com.ExecuteNonQuery();
                    con.Close();
                }
                retVal.Add(new Result { result = res });
                return retVal;
            }
            catch (Exception e)
            {
                retVal.Add(new Result { result = -1 });
                return retVal;
            }
        }

        public List<Termin> TerminBySubjectId(string idSubject)
        {
            List<Termin> list = new List<Termin>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {

                using (SqlCommand getData = new SqlCommand("SELECT id_termin FROM termin WHERE id_subject = " + Int32.Parse(idSubject), connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Termin
                            {
                                idTermin = myReader.GetInt32(0)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Termin
                            {
                                idTermin = -1

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Termin> terminInfoBySubjectId(string idSubject)
        {
                List<Termin> list = new List<Termin>();

                using (SqlConnection connection = new SqlConnection(connectionStrings))
                {

                    using (SqlCommand getData = new SqlCommand("select distinct b.id_termin,a.id_tutor, a.name, a.lastname, b.date, c.subject, d.price from subject c, tutor a, termin b, subject_tutor d " +
                        "WHERE b.id_subject = "+Int32.Parse(idSubject)+" AND b.reserved = 0 AND a.id_tutor = b.id_tutor AND c.id_subject = b.id_subject AND c.id_subject = d.id_subject AND b.id_tutor = d.id_tutor", connection))
                    {
                        connection.Open();
                        SqlDataReader myReader = getData.ExecuteReader();
                        while (myReader.Read())
                        {
                            try
                            {
                                list.Add(new Termin
                                {
                                    idTermin = myReader.GetInt32(0),
                                    idTutor = myReader.GetInt32(1),
                                    tutorName = myReader.GetString(2),
                                    tutorLastname = myReader.GetString(3),
                                    date = myReader.GetString(4),
                                    subject = myReader.GetString(5),
                                    price = myReader.GetInt32(6)

                                });
                            }
                            catch (Exception e)
                            {
                                list.Add(new Termin
                                {
                                    idTermin = -1

                                });
                            }

                        }
                        connection.Close();
                    }
                }
                return list;
            }
        
        public List<Termin> terminInfoById(string idTermin)
{
   List<Termin> list = new List<Termin>();

   using (SqlConnection connection = new SqlConnection(connectionStrings))
   {

       using (SqlCommand getData = new SqlCommand("SELECT a.id_termin, b.id_tutor, b.name, b.lastname, a.date, c.subject, d.price FROM termin a, tutor b, subject c, subject_tutor d WHERE a.id_termin = "+Int32.Parse(idTermin)+ "  AND a.id_tutor = b.id_tutor AND c.id_subject = a.id_subject AND d.id_subject = a.id_subject and d.id_tutor = a.id_tutor", connection))
       {
           connection.Open();
           SqlDataReader myReader = getData.ExecuteReader();
           while (myReader.Read())
           {
               try
               {
                   list.Add(new Termin
                   {
                       idTermin = myReader.GetInt32(0),
                       idTutor = myReader.GetInt32(1),
                       tutorName = myReader.GetString(2),
                       tutorLastname = myReader.GetString(3),
                       date = myReader.GetString(4),
                       subject = myReader.GetString(5),
                       price = myReader.GetInt32(6)

                   });
               }
               catch (Exception e)
               {
                   list.Add(new Termin
                   {
                       idTermin = -1

                   });
               }

           }
           connection.Close();
       }
   }
   return list;
}

        public List<Result> deleteTermin(string idTermin)
        {
            var retVal = new List<Result>();
            int res;
            using (SqlConnection con = new SqlConnection(connectionStrings))
            {
                con.Open();


                string query1 = "DELETE FROM termin WHERE id_termin=@idTermin";
                SqlCommand com1 = new SqlCommand(query1, con);
                com1.Parameters.AddWithValue("@idTermin", Int32.Parse(idTermin));
                res = com1.ExecuteNonQuery();

                con.Close();
            }
            retVal.Add(new Result { result = res });
            return retVal;
        }

        public List<Result> addSubjectPrice(string idTutor, string idSubject, string price)
        {
            var retVal = new List<Result>();
            int res;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionStrings))
                {
                    con.Open();

                    string query1 = "INSERT INTO subject_tutor (id_subject, id_tutor, price) VALUES (@idSubject, @idTutor, @price)";
                    SqlCommand com1 = new SqlCommand(query1, con);
                    com1.Parameters.AddWithValue("@idSubject", Int32.Parse(idSubject));
                    com1.Parameters.AddWithValue("@idTutor", Int32.Parse(idTutor));
                    com1.Parameters.AddWithValue("@price", Int32.Parse(price));


                    res = com1.ExecuteNonQuery();

                    con.Close();
                }
                retVal.Add(new Result { result = res });
                return retVal;
            } catch (Exception e)
            {
                retVal.Add(new Result { result = -1 });
                return retVal;
            }

        
        }

        public List<Subject> subjectITeach(string idTutor)
        {
            List<Subject> list = new List<Subject>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT  a.id_subject, b.subject, a.price FROM subject_tutor a, subject b WHERE b.id_subject = a.id_subject AND a.id_tutor = "+ Int32.Parse(idTutor)+"", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Subject
                            {
                                id = myReader.GetInt32(0),
                                name = myReader.GetString(1),
                                price = myReader.GetInt32(2)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Subject
                            {
                                id = -1,
                                name = "false"

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Subject> allSubjects()
        {
            List<Subject> list = new List<Subject>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT id_subject, subject FROM subject", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Subject
                            {
                                id = myReader.GetInt32(0),
                                name = myReader.GetString(1),
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Subject
                            {
                                id = -1,
                                name = "false"

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }

        public List<Termin> terminByTutorAndSubject(string idTutor, string idSubject)
        {
            List<Termin> list = new List<Termin>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT ter.id_termin, tu.name, tu.lastname, ter.date, sub.subject, c.price "+
                    "FROM termin ter, tutor tu, subject sub, subject_tutor c " +
                    "WHERE ter.id_subject = "+idSubject+" AND tu.id_tutor = "+idTutor+" AND ter.reserved = 0 " +
                    "AND sub.id_subject = ter.id_subject AND ter.id_tutor = tu.id_tutor " +
                    "AND c.id_subject = ter.id_subject and c.id_tutor = ter.id_tutor", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Termin
                            {
                                idTermin = myReader.GetInt32(0),
                                tutorName = myReader.GetString(1),
                                tutorLastname = myReader.GetString(2),
                                date = myReader.GetString(3),
                                subject = myReader.GetString(4),
                                price = myReader.GetInt32(5)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Termin
                            {
                                idTermin = -1,
                                date = "false",
                                subject = "false"

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }
        /*
        public List<Termin> terminByTutor(string idTutor, string idSubject)
        {
            List<Termin> list = new List<Termin>();

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                using (SqlCommand getData = new SqlCommand("SELECT ter.id_termin, ter.id_subject, ter.reserved, ter.date,  s.subject, p.price " +
                    "FROM termin ter, tutor t, subject s, subject_tutor p" +
                    "WHERE ter.id_tutor = 16 AND s.id_subject = ter.id_subject AND t.id_tutor = ter.id_tutor " +
                    "AND ter.id_subject = p.id_subject AND t.id_tutor = p.id_tutorr", connection))
                {
                    connection.Open();
                    SqlDataReader myReader = getData.ExecuteReader();
                    while (myReader.Read())
                    {
                        try
                        {
                            list.Add(new Termin
                            {
                                idTermin = myReader.GetInt32(0),
                                date = myReader.GetString(3),
                                subject = myReader.GetString(4),
                                price = myReader.GetInt32(5)
                            });
                        }
                        catch (Exception e)
                        {
                            list.Add(new Termin
                            {
                                idTermin = -1,
                                date = "false",
                                subject = "false"

                            });
                        }

                    }
                    connection.Close();
                }
            }
            return list;
        }*/
    }
}

