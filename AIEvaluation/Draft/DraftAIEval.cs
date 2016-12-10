﻿using System;
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
    {/// <summary>
     /// 1) group the players
     /// 2) group the personnel
     /// 3) cycle through the groups and the appropriate personnel group grades the appropriate player group
     /// </summary>
        public DraftAIEval()
        {
            var gradesList = new List<string>();
            var files = new List<string>(Directory.EnumerateFiles(@"..\..\..\AIEvaluation\Draft\Grading Sheets", "*.*", SearchOption.AllDirectories)); //load grade files

            var draftDTGroups =  //groups players by Scout Region then by Position, ordered by Grade
                    from player in DraftDT.AsEnumerable()
                    orderby player.Field<decimal>("ActualGrade") descending
                    group player by player.Field<string>("ScoutRegion") into draftGroups
                    from posGroups in
                    (from player in draftGroups
                     group player by player.Field<string>("CollegePOS"))
                    group posGroups by draftGroups.Key;

            var personnelDTGroups =  //groups personnel by Scout Region then by PersonnelType, ordered by TeamID
                   from personnel in PersonnelDT.AsEnumerable()
                   orderby personnel.Field<int>("TeamID") descending
                   group personnel by personnel.Field<string>("ScoutRegion") into personnelGroups
                   from posGroups in
                   (from personnel in personnelGroups
                    group personnel by personnel.Field<int>("PersonnelType"))
                   group posGroups by personnelGroups.Key;

            for (var i = 0; i < 7; i++)
            {
                foreach (var region in draftDTGroups)
                {
                    foreach (var persReg in personnelDTGroups)
                    {
                        if (region.Key == persReg.Key) //regions are the same
                        {
                            foreach (var pos in region) //cycle through the positions
                            {
                                foreach (var personnel in persReg) //cycle through each scout
                                {
                                    GetGrade(pos, personnel, pos.Key);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var file in files) //open each file
            {
                using (StreamReader reader = new StreamReader(file)) //read each file then loop through the grading system
                {
                    gradesList.Clear(); //clears the list
                    reader.ReadLine(); //skips the formatting line
                    var pos = reader.ReadLine().Remove(0, 5).ToString();
                    gradesList.Add(pos); //gets the position for this file on the second like
                    var playerRole = reader.ReadLine().Remove(0, 6).ToString();
                    gradesList.Add(playerRole); //gets the role of this position on the 3rd line
                    while (!reader.EndOfStream) //continues going while not at the end of the file
                    {
                        gradesList.Add(reader.ReadLine()); //reads each line into a file
                    }

                    for (int i = 0; i < PersonnelDT.Rows.Count; i++) //cycles through the scouts
                    {
                        //gradeProspects((int)DraftDT.Rows[i]["DraftID"], DraftDT.Rows[i]["ScoutRegion"].ToString(), gradesList);
                    }
                }
            }
        }

        /// <summary>
        /// Grades are generated for each prospect by each scout
        /// </summary>
        /// <returns></returns>
       /* private void gradeProspects(int playerId, string region, List<string> gradesList)
        {
            int role;
            decimal top150 = 7.5m; //grade cutoff for 150th player
            decimal top250 = 7.0m; //grade cutoff for 250th player
            decimal top350 = 6.5m; //grade cutoff for 350th player
            decimal top450 = 6.35m; //grade cutoff for 450th player

            var pos = gradesList[0];
            var playerRole = gradesList[1];
            gradesList.RemoveRange(0, 2); //removes the position and role from the list

            for (int i = 0; i < gradesList.Count; i++) //cycle through the gradesList to run each list
            {
                Parallel.For(0, PersonnelDT.Rows.Count, j =>
                { //runs this loop in parallel to check each row of the DT for the position
                    {
                        role = (int)PersonnelDT.Rows[j]["PersonnelType"];
                        switch (role)
                        {
                            case (int)GM: //check to make sure its a player with a high enough grade
                                if ((decimal)DraftDT.Rows[playerId]["ActualGrade"] >= top150)
                                {
                                    DraftGradesDT.Rows[j][playerId.ToString()] = GetGrade(playerId, j);
                                }
                                break;

                            case (int)AssistantGM:
                                if ((decimal)DraftDT.Rows[playerId]["ActualGrade"] >= top250)
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
        }*/

        /// <summary>
        /// Sends the personnel and player objects along with the position string to the algorithm.  Cycle through each player and run the grade based on the position
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="scoutID"></param>
        /// <returns></returns>
        private decimal GetGrade(IGrouping<int, DataRow> playerGroup, IGrouping<int, DataRow> personnelGroup, string pos)
        {
            switch (pos.ToString())
            {
                case "QB":

                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeQB(player, personnel);
                        }
                    }
                    break;

                case "RB":
                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeRB(player, personnel);
                        }
                    }
                    break;

                case "FB":
                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeFB(player, personnel);
                        }
                    }
                    break;

                case "WR":
                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeWR(player, personnel);
                        }
                    }
                    break;

                case "TE":
                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeTE(player, personnel);
                        }
                    }
                    break;

                case "OT":
                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeOT(player, personnel);
                        }
                    }
                    break;

                case "OG":

                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeOG(player, personnel);
                        }
                    }
                    break;

                case "C":
                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeOC(player, personnel);
                        }
                    }
                    break;

                case "DE":
                    foreach (var player in playerGroup) //cycle through each player
                    {
                        foreach (var personnel in personnelGroup) //cycle through each personnel
                        {
                            GradeDE(player, personnel);
                        }
                    }
                    break;

                case "DT"
            }
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeQB(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeRB(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeFB(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeWR(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeTE(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOT(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOG(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOC(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeDE(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeDT(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeOLB(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeILB(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeCB(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeSF(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeP(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }

        private decimal GradeK(DataRow player, DataRow personnel)
        {
            decimal grade = 0m;

            return grade;
        }
    }
}