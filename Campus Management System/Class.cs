//Students

//StudentId (PK)
//Name
//Email
//Phone
//DateOfBirth
//Gender

//Courses ki info.

//Courses

//CourseId (PK)
//CourseName
//CreditHours
//Fee

//Teachers / Admin staff.


//StaffId (PK)
//Name
//Role (Teacher / Admin)
//Email
//Phone

//4Enrollments Table ⭐ (VERY IMPORTANT)

//Ye sab se important table hai.

//Student multiple courses le sakta hai
//Course me multiple students ho sakte hain

//Is relation ko bolte hain:
//👉 Many - to - Many

//Is liye bridge table banti hai.

//Enrollments

//EnrollmentId (PK)
//StudentId (FK)
//CourseId (FK)
//EnrollmentDate
//5️⃣ Teaching Table (Staff ↔ Course)

//Ek teacher multiple courses parhata hai
//Ek course multiple teachers bhi ho sakte hain.

//CourseStaff

//Id (PK)
//StaffId (FK)
//CourseId (FK)
//6️⃣ Departments Table (Optional but PRO level)

//Project ko strong banata hai 💪

//Departments

//DepartmentId (PK)
//DepartmentName

//Then:

//Student → DepartmentId (FK)
//Staff → DepartmentId (FK)
//Course → DepartmentId (FK)