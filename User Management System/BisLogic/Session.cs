﻿using Lightaplusplus.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lightaplusplus.BisLogic
{
    public static class Session
    {
        public static void setUser(this ISession session, Users user)
        {
            session.SetInt32("UserId", user.ID);
            session.SetString("UserType", user.usertype.ToString());
            if(user.CurrentLoginTime != null)
            {
                session.SetString("UserCurrentLogin", user.CurrentLoginTime.ToString());
            }
            if(user.LastLoginTime != null)
            {
                session.SetString("UserLastLogin", user.LastLoginTime.ToString());
            }
        }

        public static int? getUserId(this ISession session)
        {
            return session.GetInt32("UserId");
        }

        public static string getUserType(this ISession session)
        {
            return session.GetString("UserType");
        }

        public static DateTime? getUserCurrentLogin(this ISession session)
        {
            var login = session.GetString("UserCurrentLogin");
            if (login == null) return null;

            return (DateTime?)DateTime.Parse(login);
        }

        public static DateTime? getUserLastLogin(this ISession session)
        {
            var login = session.GetString("UserLastLogin");
            if (login == null) return null;

            return (DateTime?)DateTime.Parse(login);
        }

        public static void setSections(this ISession session, string sections)
        {
            session.SetString("Sections", sections);
        }

        public static List<Sections> getSections(this ISession session)
        {
            string mySections = session.GetString("Sections");
            List<Sections> sectionsList = new List<Sections>();
            foreach (var section in mySections.Split(":::"))
            {
                try
                {
                    if (section.Length > 0)
                    {
                        Sections mySection = JsonConvert.DeserializeObject<Sections>(section);
                        sectionsList.Add(mySection);
                    }
                }
                catch
                {
                    continue;
                }
            }
            
            return sectionsList;
        }

        public static void setAllSections(this ISession session, string sections)
        {
            session.SetString("AllSections", sections);
        }

        public static List<Sections> getAllSections(this ISession session)
        {
            string mySections = session.GetString("AllSections");
            List<Sections> sectionsList = new List<Sections>();
            foreach (var section in mySections.Split(":::"))
            {
                try
                {
                    if (section.Length > 0)
                    {
                        Sections mySection = JsonConvert.DeserializeObject<Sections>(section);
                        sectionsList.Add(mySection);
                    }
                }
                catch
                {
                    continue;
                }
            }

            return sectionsList;
        }

        public static List<Assignments> getAssignments(this ISession session)
        {
            List<Sections> sections = Session.getSections(session);
            List<Assignments> myAssignments = new List<Assignments>();
            foreach (var section in sections)
            {
                if (section != null)
                {
                    foreach (var assignment in section.Assignments)
                    {
                        myAssignments.Add(assignment);
                    }
                }
                else
                {
                    continue;
                }
            }

            return myAssignments;
        }
    }
}
