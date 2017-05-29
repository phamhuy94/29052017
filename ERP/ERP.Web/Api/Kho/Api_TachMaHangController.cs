using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERP.Web.Api.Kho
{

    public class TachGopMa
    {
        public KHO_INIT_TACH_GOP_MA TachGop { get; set; }
        public KHO_NHAT_KY_TACH_GOP_MA NhatKyTachGop { get; set; }
    }

    public class Api_TachMaHangController : ApiController
    {


        // GET: api/Api_DM_KHO
        public IQueryable<DM_KHO> GetDM_KHO()
        {
            using (var db = new ERP_DATABASEEntities())
            {
                return db.DM_KHO;
            }
                
        }

        // POST: api/Api_DM_KHO
        [HttpPost]
        [Route("api/Api_TachMaHang/TachMaHang/{makho}")]
        public IHttpActionResult TachMaHang(string makho,TachGopMa tachgopma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ERP_DATABASEEntities())
            {
               
                KHO_INIT_TACH_GOP_MA tachma = tachgopma.TachGop;
                KHO_NHAT_KY_TACH_GOP_MA nhatkytachma = tachgopma.NhatKyTachGop;



                #region "Trường hợp 3 mã cùng tách"
                if (tachma.MA_HANG_GOC_1 != null && tachma.MA_HANG_GOC_2 != null && tachma.MA_HANG_GOC_3 != null)
                {
                    var query = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_GOC_1 && x.MA_KHO_CON == makho).FirstOrDefault();
                    var query1 = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_GOC_2 && x.MA_KHO_CON == makho).FirstOrDefault();
                    var query2 = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_GOC_3 && x.MA_KHO_CON == makho).FirstOrDefault();
                    if (query != null && query1 != null && query2 != null)
                    {

                        //Muốn tách  mã được thì SL tồn trong kho phải lớn hơn SL muốn tách
                        if (query.SL_HOPLONG > tachma.SL_MA_GOC_1 && query1.SL_HOPLONG > tachma.SL_MA_GOC_2 && query2.SL_HOPLONG > tachma.SL_MA_GOC_3)
                        {
                            query.SL_HOPLONG = query.SL_HOPLONG - (int)tachma.SL_MA_GOC_1;
                            query1.SL_HOPLONG = query1.SL_HOPLONG - (int)tachma.SL_MA_GOC_2;
                            query2.SL_HOPLONG = query2.SL_HOPLONG - (int)tachma.SL_MA_GOC_3;


                            if (tachma.MA_HANG_DICH_25 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_25 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_25;
                            }

                            if (tachma.MA_HANG_DICH_24 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_24 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_24;
                            }

                            if (tachma.MA_HANG_DICH_23 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_23 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_23;
                            }
                            if (tachma.MA_HANG_DICH_22 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_22 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_22;
                            }
                            if (tachma.MA_HANG_DICH_21 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_21 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_21;

                            }
                            if (tachma.MA_HANG_DICH_20 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_20 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_20;
                            }

                            if (tachma.MA_HANG_DICH_19 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_19 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_19;
                            }

                            if (tachma.MA_HANG_DICH_18 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_18 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_18;
                            }
                            if (tachma.MA_HANG_DICH_17 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_17 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_17;
                            }
                            if (tachma.MA_HANG_DICH_16 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_16 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_16;

                            }
                            if (tachma.MA_HANG_DICH_14 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_14 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_14;
                            }

                            if (tachma.MA_HANG_DICH_15 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_15 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_15;
                            }

                            if (tachma.MA_HANG_DICH_13 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_13 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_13;
                            }
                            if (tachma.MA_HANG_DICH_12 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_12 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_12;
                            }
                            if (tachma.MA_HANG_DICH_11 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_11 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_11;

                            }
                            if (tachma.MA_HANG_DICH_10 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_10 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_10;
                            }

                            if (tachma.MA_HANG_DICH_9 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_9 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_9;
                            }

                            if (tachma.MA_HANG_DICH_8 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_8 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_8;
                            }
                            if (tachma.MA_HANG_DICH_7 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_7 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_7;
                            }
                            if (tachma.MA_HANG_DICH_6 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_6 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_6;

                            }
                            if (tachma.MA_HANG_DICH_5 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_5 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_5;
                            }

                            if (tachma.MA_HANG_DICH_4 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_4 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_4;
                            }

                            if (tachma.MA_HANG_DICH_3 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_3 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_3;
                            }
                            if (tachma.MA_HANG_DICH_2 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_2 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_2;
                            }
                            if (tachma.MA_HANG_DICH_1 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_1 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_1;

                            }

                        }



                    }


                }
                #endregion



                #region "Trường hợp 3 mã cùng tách"
                if (tachma.MA_HANG_GOC_1 != null && tachma.MA_HANG_GOC_2 != null && tachma.MA_HANG_GOC_3 == null)
                {
                    var query = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_GOC_1 && x.MA_KHO_CON == makho).FirstOrDefault();
                    var query1 = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_GOC_2 && x.MA_KHO_CON == makho).FirstOrDefault();
                    if (query != null && query1 != null)
                    {

                        //Muốn tách  mã được thì SL tồn trong kho phải lớn hơn SL muốn tách
                        if (query.SL_HOPLONG > tachma.SL_MA_GOC_1 && query1.SL_HOPLONG > tachma.SL_MA_GOC_2)
                        {
                            query.SL_HOPLONG = query.SL_HOPLONG - (int)tachma.SL_MA_GOC_1;
                            query1.SL_HOPLONG = query1.SL_HOPLONG - (int)tachma.SL_MA_GOC_2;


                            if (tachma.MA_HANG_DICH_25 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_25 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_25;
                            }

                            if (tachma.MA_HANG_DICH_24 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_24 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_24;
                            }

                            if (tachma.MA_HANG_DICH_23 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_23 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_23;
                            }
                            if (tachma.MA_HANG_DICH_22 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_22 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_22;
                            }
                            if (tachma.MA_HANG_DICH_21 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_21 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_21;

                            }
                            if (tachma.MA_HANG_DICH_20 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_20 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_20;
                            }

                            if (tachma.MA_HANG_DICH_19 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_19 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_19;
                            }

                            if (tachma.MA_HANG_DICH_18 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_18 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_18;
                            }
                            if (tachma.MA_HANG_DICH_17 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_17 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_17;
                            }
                            if (tachma.MA_HANG_DICH_16 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_16 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_16;

                            }
                            if (tachma.MA_HANG_DICH_14 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_14 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_14;
                            }

                            if (tachma.MA_HANG_DICH_15 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_15 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_15;
                            }

                            if (tachma.MA_HANG_DICH_13 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_13 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_13;
                            }
                            if (tachma.MA_HANG_DICH_12 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_12 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_12;
                            }
                            if (tachma.MA_HANG_DICH_11 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_11 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_11;

                            }
                            if (tachma.MA_HANG_DICH_10 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_10 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_10;
                            }

                            if (tachma.MA_HANG_DICH_9 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_9 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_9;
                            }

                            if (tachma.MA_HANG_DICH_8 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_8 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_8;
                            }
                            if (tachma.MA_HANG_DICH_7 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_7 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_7;
                            }
                            if (tachma.MA_HANG_DICH_6 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_6 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_6;

                            }
                            if (tachma.MA_HANG_DICH_5 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_5 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_5;
                            }

                            if (tachma.MA_HANG_DICH_4 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_4 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_4;
                            }

                            if (tachma.MA_HANG_DICH_3 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_3 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_3;
                            }
                            if (tachma.MA_HANG_DICH_2 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_2 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_2;
                            }
                            if (tachma.MA_HANG_DICH_1 != null)
                            {
                                var data = db.TONKHO_HOPLONG.Where(x => x.MA_HANG == tachma.MA_HANG_DICH_1 && x.MA_KHO_CON == makho).FirstOrDefault();
                                data.SL_HOPLONG = data.SL_HOPLONG + (int)tachma.SL_MA_DICH_1;

                            }

                        }



                    }


                }
                #endregion


                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {

                    throw;
                }

                return Ok();

            }


            
        }
    }
}
