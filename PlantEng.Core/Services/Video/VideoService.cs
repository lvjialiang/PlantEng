using PlantEng.Models.Video;
using PlantEng.Data.Video;
using PlantEng.Models;
using System.Collections.Generic;

namespace PlantEng.Services.Video
{
    public  static class VideoService
    {
        /// <summary>
        /// 添加视频
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回VideoId</returns>
        public static VideoInfo Create(VideoInfo model)
        {
            if (model.Id == 0)
            {
                int id = VideoManage.Insert(model);
                model.Id = id;
            }
            else {
                VideoManage.Update(model);
            }
            return model;
        }
        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="videoId"></param>
        public static void UpdateViewCount(int videoId) {
            VideoManage.UpdateViewCount(videoId);
        }
        /// <summary>
        /// 更新播放次数
        /// </summary>
        /// <param name="videoId"></param>
        public static void UpdatePlayCount(int videoId)
        {
            VideoManage.UpdatePlayCount(videoId);
        }
        /// <summary>
        /// 获得视频信息
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public static VideoInfo GetById(int videoId)
        {
            return VideoManage.Get(videoId);
        }
        public static IList<VideoInfo> ListWithoutPage(int topCount = 10) {
            return VideoManage.ListWithoutPage(topCount);
        }
        /// <summary>
        /// 视频列表
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IPageOfList<VideoInfo> List(VideoSearchSetting setting)
        {
            return VideoManage.List(setting);
        }
    }
}
