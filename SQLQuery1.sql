select * from PrizeWith where idClass=42
select * from Exam where idClass=65 and studentEmail='daniela@gmail.com'
select * from Remarks where idClass=42 
select * from RandomAddPoints where idClass=42
select * from CounterPoints where idClass=42

 UPDATE Exam SET grade=90 where idclass=35 and examNum=3 and studentEmail='daniela@gmail.com'

 delete from exam  where grade = (select top 1 grade FROM exam where idClass=35 and studentEmail='daniela@gmail.com' order by grade asc)

 DELETE FROM exam where grade= (select top 1 grade FROM exam where idClass=35 and studentEmail='daniela@gmail.com'order by grade asc)

 select * from exam where idClass =35
 select * from remarks where idClass =35
 select * from PurchasesPrizes
 select * from Student where email='daniela@gmail.com'
 select * from student
 select * from studentRemark where emailStudent='daniela@gmail.com'

 insert into exam (idclass, examNum, studentEmail,grade) values(65,2,'daniela@gmail.com',80)

 delete from studentRemark where emailStudent='daniela@gmail.com' and id=20
 delete from Student where  id=32 

  UPDATE Student SET studentEmail='tomer@gmail.com' where idclass=65 and  studentEmail='tomerlior1360@gmail.com'
  UPDATE studentRemark SET remarkName='אמר שדניאלה טועה' where emailStudent='daniela@gmail.com' and id=2



  DELETE FROM studentRemark where studentRemark.emailStudent = 'daniela@gmail.com' and studentRemark.remarkName = (select top 1 remarkname
																													from Remarks
																													where idClass = 35 
																													order by takenpoints asc)

select * FROM studentRemark where studentRemark.emailStudent = 'daniela@gmail.com' and studentRemark.remarkName = (select top 1 with ties remarkname
from Remarks
where idClass = 35 
order by takenpoints)


DELETE FROM studentRemark  where studentRemark.emailStudent = 'daniela@gmail.com' and studentRemark.remarkName = (select top 1 remarkname FROM remarks where idClass=35 order by takenpoints asc)



select remarkname FROM remarks where idClass=35 group by remarkname having max(takenpoints)<= ALL (select takenpoints from remarks where idClass=35)


select * FROM studentRemark  where studentRemark.emailStudent = 'daniela@gmail.com' and exists (select remarkname FROM remarks where idClass=35 order by takenpoints asc)


select * FROM studentRemark  where studentRemark.emailStudent = 'daniela@gmail.com' and studentRemark.remarkName = (select  top 1 sr.remarkName FROM studentRemark sr inner join Remarks r on sr.idClass = r.idClass order by takenpoints asc)

select  top 1 sr.remarkName FROM studentRemark sr inner join Remarks r on sr.idClass=r.idClass

select * FROM studentRemark 
select * FROM Remarks 

insert into studentRemark (idClass, emailStudent, remarkName) values (35,'daniela@gmail.com' , 'אלימות' )

select * FROM studentRemark  where studentRemark.emailStudent = 'daniela@gmail.com' and studentRemark.idClass=35 and studentRemark.remarkName = (select top 1 sr.remarkName FROM studentRemark sr inner join Remarks r on sr.idClass = r.idClass order by takenpoints asc)
delete FROM studentRemark  where studentRemark.emailStudent = 'daniela@gmail.com' and studentRemark.idClass=35 and studentRemark.remarkName = (select  top 1 sr.remarkName FROM studentRemark sr inner join Remarks r on sr.idClass = r.idClass order by takenpoints asc)


select  * FROM studentRemark sr inner join Remarks r on sr.idClass = r.idClass order by takenpoints asc


select  top 1 sr.remarkName FROM studentRemark sr inner join Remarks r on sr.remarkName = r.remarkname order by takenpoints asc

select * FROM studentRemark  where studentRemark.emailStudent = 'daniela@gmail.com' and studentRemark.idClass=35

delete from studentRemark where studentRemark.remarkName = 
(select top 1 sr.remarkName FROM studentRemark sr inner join Remarks r on sr.remarkName = r.remarkname  where sr.emailStudent = 'daniela@gmail.com' and sr.idClass=35 order by takenpoints asc)

 select * from Exam where idclass=65 and studentEmail='daniela@gmail.com'

 delete from exam insert into Exam (idclass, examNum, studentEmail, grade) values('65', '1', 'daniela@gmail.com', '80')delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '2', 'daniela@gmail.com', '60',)delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '3', 'daniela@gmail.com', '90',)delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '1', 'maor@gmail.com', '90',)delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '2', 'maor@gmail.com', '70',)delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '3', 'maor@gmail.com', '100',)delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '1', 'tomer@gmail.com', '95',)delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '2', 'tomer@gmail.com', '60',)delete from exam insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade)  insert into Exam (idclass, examNum, studentEmail, grade) values('65', '3', 'tomer@gmail.com', '80',)

 SELECT distinct s.*, sc.idClass FROM Student s inner join StudentInClass sc on s.email=sc.email where sc.idclass=65

  case "ביטול הערה": {
                        //if (Remarks.length == 0) {
                        //    swal({
                        //        title: "לא קיימות הערות במערכת, כל הכבוד ",
                        //    });
                        //}
                        //else {
                            Id = classId;
                            StudentEmail = studentData.Email;
                            let url = "../api/Classes/DeleteOneRemark?id=" + Id + "&emailStudent=" + StudentEmail;
                            ajaxCall("DELETE", url, "", DeleteOneExamSuccess2, getError);
                            return false;
                            break;
                        //}


                    }
