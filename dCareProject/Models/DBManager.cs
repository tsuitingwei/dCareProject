using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dCareProject.Models;


namespace dCareProject.Models {
    public class DBManager {
        dCareEntities db = new dCareEntities();
        List<預約表> viewList = new List<預約表>();

        public List<預約表> getLastRes {
            get {
                var query = (from o in db.預約表
                             join c in db.醫生 on o.醫生ID equals c.ID
                             orderby o.ID descending
                             select o).Take(1);
                return query.ToList();
            }
        }
    }
    public class DocManger {
        dCareEntities db = new dCareEntities();
        List<醫生> viewList = new List<醫生>();

        public List<醫生> getDoc {
            get {
                var query = from o in db.醫生
                            orderby o.ID descending
                            select o;
                return query.ToList();
            }
        }
    }

    public class PtManger {
        dCareEntities db = new dCareEntities();
        List<醫生> viewList = new List<醫生>();

        public List<醫生> getPt {
            get {
                var query = from o in db.醫生
                            where o.ID == 10
                            select o;

                return query.ToList();
            }


        }
    }
    public class PidManger {
        dCareEntities db = new dCareEntities();
        List<醫生> viewList = new List<醫生>();

        public List<醫生> getPid {
            get {
                var query = from o in db.醫生
                            join p in db.預約表 on o.ID equals p.醫生ID

                            select o;

                return query.ToList();
            }
        }
    }
    public class LkManger {
        dCareEntities db = new dCareEntities();
        List<看診紀錄表> viewList = new List<看診紀錄表>();

        public List<看診紀錄表> getlkm {
            get {
                var query = from o in db.看診紀錄表
                            
                           

                            select o;

                return query.ToList();
            }
        }
    }
    public class GDManger {
        dCareEntities db = new dCareEntities();
        List<醫生> viewList = new List<醫生>();

        public List<醫生> getgdm {
            get {
                var query = from p in db.醫生
                            where p.ID == 1 | p.ID == 3 | p.ID == 8 | p.ID == 9
                            select p;

                return query.ToList();
            }
        }
    }

}
    