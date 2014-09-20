using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using PlantEng.Models;
using PlantEng.Models.Video;
using Goodspeed.Library.Data;
using PlantEng.Common;

namespace PlantEng.Data.Video
{
    public static class VideoManage
    {
        /// <summary>
        /// 添加视频
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回VideoId</returns>
        public static int Insert(VideoInfo model) {
            string strSQL = "INSERT INTO Videos(CategoryId,Title,Remark,VideoUrl,ImageUrl,IsTop,Tags,PublishDateTime,IsDeleted) VALUES(@CategoryId,@Title,@Remark,@VideoUrl,@ImageUrl,@IsTop,@Tags,@PublishDateTime,@IsDeleted);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CategoryId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("VideoUrl",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("IsTop",SqlDbType.Int),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("IsDeleted",SqlDbType.Int),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CategoryId;
            parms[2].Value = string.IsNullOrEmpty(model.Title) ? string.Empty : model.Title;
            parms[3].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[4].Value = string.IsNullOrEmpty(model.VideoUrl) ? string.Empty : model.VideoUrl;
            parms[5].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[6].Value = model.IsTop ? 1 : 0;
            parms[7].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[8].Value = model.PublishDateTime;
            parms[9].Value = model.IsDeleted ? 1 : 0;
            return Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        /// <summary>
        /// 更新视频
        /// </summary>
        /// <param name="model">返回影响的行数</param>
        public static int Update(VideoInfo model) {
            string strSQL = "UPDATE Videos SET CategoryId = @CategoryId ,Title = @Title,Remark = @Remark,VideoUrl = @VideoUrl,ImageUrl = @ImageUrl,IsTop = @IsTop,Tags = @Tags ,PublishDateTime = @PublishDateTime ,IsDeleted = @IsDeleted WHERE Id = @Id";
            SqlParameter[] parms = { 
                                    new SqlParameter("Id",SqlDbType.Int),
                                    new SqlParameter("CategoryId",SqlDbType.Int),
                                    new SqlParameter("Title",SqlDbType.NVarChar),
                                    new SqlParameter("Remark",SqlDbType.NVarChar),
                                    new SqlParameter("VideoUrl",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("IsTop",SqlDbType.Int),
                                    new SqlParameter("Tags",SqlDbType.NVarChar),
                                    new SqlParameter("PublishDateTime",SqlDbType.DateTime),
                                    new SqlParameter("IsDeleted",SqlDbType.Int),
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CategoryId;
            parms[2].Value = string.IsNullOrEmpty(model.Title) ? string.Empty : model.Title;
            parms[3].Value = string.IsNullOrEmpty(model.Remark) ? string.Empty : model.Remark;
            parms[4].Value = string.IsNullOrEmpty(model.VideoUrl) ? string.Empty : model.VideoUrl;
            parms[5].Value = string.IsNullOrEmpty(model.ImageUrl) ? string.Empty : model.ImageUrl;
            parms[6].Value = model.IsTop ? 1 : 0;
            parms[7].Value = string.IsNullOrEmpty(model.Tags) ? string.Empty : model.Tags;
            parms[8].Value = model.PublishDateTime;
            parms[9].Value = model.IsDeleted ? 1 : 0;
            return SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parms);
        }
        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="videoId"></param>
        public static void UpdateViewCount(int videoId) {
            string strSQL = "UPDATE Videos SET ViewCount = ViewCount + 1 WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id",videoId);
            SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parm);
        }
        /// <summary>
        /// 更新播放次数
        /// </summary>
        /// <param name="videoId"></param>
        public static void UpdatePlayCount(int videoId) {
            string strSQL = "UPDATE Videos SET PlayCount = PlayCount + 1 WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id", videoId);
            SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL, parm);
        }
        /// <summary>
        /// 获得视频信息
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public static VideoInfo Get(int videoId) {
            string strSQL = "SELECT TOP(1)* FROM Videos WITH(NOLOCK) WHERE Id = @Id";
            SqlParameter parm = new SqlParameter("Id",videoId);
            DataRow dr = SQLPlus.ExecuteDataRow(CommandType.Text,strSQL,parm);
            return Get(dr);
        }
        public static IList<VideoInfo> ListWithoutPage(int topCount = 10) {
            string strSQL = string.Format("SELECT TOP({0}) * FROM Videos ORDER BY PublishDateTime DESC",topCount);
            IList<VideoInfo> list = new List<VideoInfo>();
            VideoInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text,strSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model = Get(dr);
                    if (model != null)
                    {
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 视频列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<VideoInfo> List(VideoSearchSetting setting) {
            FastPaging fp = new FastPaging();
            fp.OverOrderBy = " PublishDateTime DESC";
            fp.PageIndex = setting.PageIndex;
            fp.PageSize = setting.PageSize;
            fp.QueryFields = "*";
            fp.TableName = "Videos";
            fp.PrimaryKey = "Id";
            fp.WithOptions = " WITH(NOLOCK)";
            fp.Condition = " 1 = 1 ";
            if(!setting.ShowDeleted){
                fp.Condition += "   AND IsDeleted = 0";
            }
            if(setting.CategoryId > 0){
                fp.Condition += string.Format(" AND CategoryId = {0}",setting.CategoryId);
            }

            IList<VideoInfo> list = new List<VideoInfo>();
            VideoInfo model = null;
            DataTable dt = SQLPlus.ExecuteDataTable(CommandType.Text, fp.Build2005());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    model = Get(dr);
                    if (model != null)
                    {
                        list.Add(model);
                    }
                }
            }
            int count = Convert.ToInt32(SQLPlus.ExecuteScalar(CommandType.Text, fp.BuildCountSQL()));
            return new PageOfList<VideoInfo>(list, setting.PageIndex, setting.PageSize, count);
        }
        private static VideoInfo Get(DataRow dr) {
            if (dr == null) { return new VideoInfo(); }
            return new VideoInfo() {             
                Id = dr.Field<int>("Id"),
                CategoryId = dr.Field<int>("CategoryId"),
                Title = dr.Field<string>("Title"),
                VideoUrl = dr.Field<string>("VideoUrl"),
                Remark = dr.Field<string>("Remark"),
                ImageUrl = dr.Field<string>("ImageUrl"),
                PlayCount = dr.Field<int>("PlayCount"),
                ViewCount = dr.Field<int>("ViewCount"),
                IsTop = dr.Field<bool>("IsTop"),
                PublishDateTime = dr.Field<DateTime>("PublishDateTime"),
                CreateDateTime = dr.Field<DateTime>("CreateDateTime"),
                Tags = dr.Field<string>("Tags"),
                IsDeleted = dr.Field<bool>("IsDeleted")
            };
        }
    }
}
