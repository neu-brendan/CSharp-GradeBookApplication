using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            this.Type = GradeBookType.Ranked;
        }

        public override void CalculateStatistics()
        {
            if (this.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (this.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if ( this.Students.Count < 5 )
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var studentOrderedByAvgGrades = this.Students.OrderBy( x => x.AverageGrade);

            // [95,84,83,82,81,95,84,83,82,82]
            //   ^ A
            //      ^ B
            // # students * 20% = 
            // 5 * .2 = 1
            // 6 * .2 = 1.2
            // 7 * .2 = 1.4
            // 8 * .2 = 1.6
            // 9 * .2 = 1.8
            // 10* .2 = 2.0
            var twentyPercent = .2;
            var numberOfStudentsToDropLetterGrade = this.Students.Count * twentyPercent;
            var numberGradesHigherThanArgument = 0;

            foreach(var student in studentOrderedByAvgGrades)
            {
                if (student.AverageGrade > averageGrade)
                    numberGradesHigherThanArgument++;
            }

            var grade = numberGradesHigherThanArgument / numberOfStudentsToDropLetterGrade;

            if (grade == 0)
                return 'A';
            else if (grade == 1)
                return 'B';
            else if (grade == 2)
                return 'C';
            else if (grade == 3)
                return 'D';
            else
                return 'F';
        }

    }
}