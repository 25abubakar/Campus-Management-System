//using Microsoft.AspNetCore.Http.HttpResults;
//using System.Collections;
//using System.ComponentModel;
//using System.Security.Principal;

//Got it — you want a complete SQL script with:

//Tables
//Primary Keys
//Foreign Keys
//CHECK constraints
//Other useful constraints (UNIQUE, NOT NULL, etc.)

//Here’s a clean, production-style version 👇

//✅ Full Database Schema (SQL)
//-- =========================
//-- STUDENTS TABLE
//-- =========================
//CREATE TABLE Students (
//    StudentId INT PRIMARY KEY IDENTITY(1,1),
//    Name VARCHAR(100) NOT NULL,
//    Email VARCHAR(100) UNIQUE NOT NULL,
//    Phone VARCHAR(20) UNIQUE,
//    DateOfBirth DATE NOT NULL,
//    Gender VARCHAR(10) NOT NULL,

//    CONSTRAINT chk_gender 
//        CHECK (Gender IN ('Male', 'Female', 'Other')),

//    CONSTRAINT chk_dob 
//        CHECK (DateOfBirth <= GETDATE())
//);
//-- =========================
//--COURSES TABLE
//-- =========================
//CREATE TABLE Courses(
//    CourseId INT PRIMARY KEY IDENTITY(1,1),
//    CourseName VARCHAR(100) NOT NULL,
//    CreditHours INT NOT NULL,
//    Fee DECIMAL(10,2) NOT NULL,

//    CONSTRAINT chk_credit_hours 
//        CHECK (CreditHours BETWEEN 1 AND 6),

//    CONSTRAINT chk_fee 
//        CHECK (Fee >= 0)
//);
//-- =========================
//--STAFF TABLE
//-- =========================
//CREATE TABLE Staff(
//    StaffId INT PRIMARY KEY IDENTITY(1,1),
//    Name VARCHAR(100) NOT NULL,
//    Role VARCHAR(20) NOT NULL,
//    Email VARCHAR(100) UNIQUE NOT NULL,
//    Phone VARCHAR(20) UNIQUE,

//    CONSTRAINT chk_role 
//        CHECK (Role IN ('Teacher', 'Admin'))
//);
//-- =========================
//--ENROLLMENTS TABLE
//-- =========================
//CREATE TABLE Enrollments(
//    EnrollmentId INT PRIMARY KEY IDENTITY(1,1),
//    StudentId INT NOT NULL,
//    CourseId INT NOT NULL,
//    EnrollmentDate DATE DEFAULT GETDATE(),

//    CONSTRAINT fk_student
//        FOREIGN KEY (StudentId) 
//        REFERENCES Students(StudentId)
//        ON DELETE CASCADE,

//    CONSTRAINT fk_course
//        FOREIGN KEY (CourseId) 
//        REFERENCES Courses(CourseId)
//        ON DELETE CASCADE,

//    CONSTRAINT uq_student_course 
//        UNIQUE (StudentId, CourseId)
//);
//-- =========================
//--COURSE ASSIGNMENTS TABLE
//-- =========================
//CREATE TABLE CourseAssignments (
//    AssignmentId INT PRIMARY KEY IDENTITY(1,1),
//    CourseId INT NOT NULL,
//    StaffId INT NOT NULL,

//    CONSTRAINT fk_course_assignment
//        FOREIGN KEY (CourseId) 
//        REFERENCES Courses(CourseId)
//        ON DELETE CASCADE,

//    CONSTRAINT fk_staff_assignment
//        FOREIGN KEY (StaffId) 
//        REFERENCES Staff(StaffId)
//        ON DELETE CASCADE
//);
//-- =========================
//--GRADES TABLE
//-- =========================
//CREATE TABLE Grades(
//    GradeId INT PRIMARY KEY IDENTITY(1,1),
//    EnrollmentId INT NOT NULL,
//    Grade VARCHAR(2),

//    CONSTRAINT fk_enrollment
//        FOREIGN KEY (EnrollmentId) 
//        REFERENCES Enrollments(EnrollmentId)
//        ON DELETE CASCADE,

//    CONSTRAINT chk_grade 
//        CHECK (Grade IN ('A', 'B', 'C', 'D', 'F'))
//);
//✅ SELECT Query(Joining All Tables)

//This query shows:

//Student info
//Course info
//Teacher
//Grade
//SELECT 
//    s.StudentId,
//    s.Name AS StudentName,
//    s.Email,
//    c.CourseName,
//    c.CreditHours,
//    st.Name AS TeacherName,
//    st.Role,
//    g.Grade
//FROM Students s
//JOIN Enrollments e ON s.StudentId = e.StudentId
//JOIN Courses c ON e.CourseId = c.CourseId
//LEFT JOIN CourseAssignments ca ON c.CourseId = ca.CourseId
//LEFT JOIN Staff st ON ca.StaffId = st.StaffId
//LEFT JOIN Grades g ON e.EnrollmentId = g.EnrollmentId;
//🔥 What This Includes
//✅ PK (Primary Keys)
//✅ FK (Foreign Keys with CASCADE)
//✅ CHECK constraints
//✅ UNIQUE constraints
//✅ DEFAULT values
//✅ Full JOIN query

//If you want next level 💡
//I can:

//Add triggers(auto grade calc, fee validation)
//Write stored procedures
//Or convert this into MySQL / Oracle version