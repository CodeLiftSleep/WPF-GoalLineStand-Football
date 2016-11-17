using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Generation.FilesAndDataTables.PersonnelType; //allows use of PersonnelType Enum's without having to declare the full name
using static GlobalResources.SharedFiles; //allows use of Global resource files without having to declare the full name

namespace AIEvaluation.Draft
{
    //Need to pull grades from the players and then evaluate them and stick them in a DB.  Grades will be updated at various points of the season

    public class DraftAIEval
    {
        public DraftAIEval()
        {
            var GradesList = new List<string>();
            var Files = new List<string>(Directory.EnumerateFiles(@"..\..\..\AIEvaluation\Draft\Grading Sheets", "*.*", SearchOption.AllDirectories)); //load grade files

            var DraftDTGroups =  //groups players by Scout Region then by Position, ordered by Grade
                    from player in DraftDT.AsEnumerable()
                    orderby player.Field<decimal>("ActualGrade") descending
                    group player by player.Field<string>("ScoutRegion") into draftGroups
                    from posGroups in
                    (from player in draftGroups
                     group player by player.Field<string>("CollegePOS"))
                    group posGroups by draftGroups.Key;

            foreach (var file in Files) //open each file
            {
                using (StreamReader Reader = new StreamReader(file)) //read each file then loop through the grading system
                {
                    GradesList.Clear(); //clears the list
                    Reader.ReadLine(); //skips the formatting line
                    var pos = Reader.ReadLine().Remove(0, 5).ToString();
                    GradesList.Add(pos); //gets the position for this file on the second like
                    var playerRole = Reader.ReadLine().Remove(0, 6).ToString();
                    GradesList.Add(playerRole); //gets the role of this position on the 3rd line
                    while (!Reader.EndOfStream) //continues going while not at the end of the file
                    {
                        GradesList.Add(Reader.ReadLine()); //reads each line into a file
                    }

                    for (int i = 0; i < PersonnelDT.Rows.Count; i++) //cycles through the scouts
                    {
                        GradeProspects((int)DraftDT.Rows[i]["DraftID"], DraftDT.Rows[i]["ScoutRegion"].ToString(), GradesList);
                    }
                }
            }
        }

        /// <summary>
        /// Grades are generated for each prospect by each scout
        /// </summary>
        /// <returns></returns>
        private void GradeProspects(int playerId, string region, List<string> gradesList)
        {
            int Role;
            decimal Top150 = 0.0m; //grade cutoff for 150th player
            decimal Top250 = 0.0m; //grade cutoff for 250th player
            decimal Top350 = 0.0m; //grade cutoff for 350th player
            decimal Top450 = 0.0m; //grade cutoff for 450th player

            var pos = gradesList[0];
            var PlayerRole = gradesList[1];
            gradesList.RemoveRange(0, 2); //removes the position and role from the list

            for (int i = 0; i < gradesList.Count; i++) //cycle through the gradesList to run each list
            {
                Parallel.For(0, PersonnelDT.Rows.Count, j =>
                { //runs this loop in parallel to check each row of the DT for the position
                    {
                        Role = (int)PersonnelDT.Rows[j]["PersonnelType"];
                        switch (Role)
                        {
                            case (int)GM: //check to make sure its a player with a high enough grade
                                if ((decimal)DraftDT.Rows[playerId]["ActualGrade"] >= Top150)
                                {
                                    DraftGradesDT.Rows[j][playerId.ToString()] = GetGrade(playerId, j);
                                }
                                break;

                            case (int)AssistantGM:
                                if ((decimal)DraftDT.Rows[playerId]["ActualGrade"] >= Top250)
                                {
                                    DraftGradesDT.Rows[j][playerId.ToString()] = GetGrade(playerId, j);
                                }
                                break;

                            case (int)DirectorPlayerPersonnel:
                                break;

                            case (int)AssistantDirPlayerPersonnel:
                                break;

                            case (int)DirectorCollegeScouting:  //determine how many players they scout
                                break;

                            case (int)AssistantDirCollegeScouting:
                                break;

                            case (int)NationalCollegeScout:
                                break;

                            case (int)AreaScout: //Check to make sure its the proper region
                                if (PersonnelDT.Rows[j]["ScoutRegion"].ToString() == region)
                                {
                                    DraftGradesDT.Rows[j][playerId.ToString()] = GetGrade(playerId, j);
                                }

                                break;

                            case (int)NatScoutingOrgScout:
                                break;
                        }
                        //make sure the player is from the same region as the scout and they have a base grade higher than a 7th round prospect
                        if (region == PersonnelDT.Rows[j]["ScoutRegion"].ToString())
                        {
                            //TODO: Add 2 DT's to the DB: DraftGradesDT and ProGradesDT...j as PK, PlayerID as columns
                            //TODO: Add ScoutRegion to the CollegePlayer DB
                            DraftGradesDT.Rows[j][playerId.ToString()] = GetGrade(playerId, j);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Runs information through the algorithm and determines the grade for the player by that Personnel person
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="scoutID"></param>
        /// <returns></returns>
        private decimal GetGrade(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeQB(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeRB(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeFB(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeWR(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeTE(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOT(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOG(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOC(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeDE(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeDT(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOLB(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeILB(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeCB(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeSF(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeP(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeK(int playerId, int personnelId)
        {
            decimal grade = 0m;

            return grade;
        }
    }
}