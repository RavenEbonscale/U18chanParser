
namespace U18chanParser
{
    internal interface IU18Parser
    {


        /// <summary>
        /// Simply Gets Image Urls from U18Chan
        /// </summary>
        /// <param name="url"></param>
        /// <returns>List of of Of image Urls</returns>
        List<U18Info> U18SimpleParse(List<string> urls);
        
        
        /// <summary>
        /// Gets both images and comments form U18chan 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        List<U18Info> U18Parse(List<string> urls);
    }
}
