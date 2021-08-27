using DoYs.Framework.Util;

using System.Windows.Forms;

namespace Repository.Common {
    public static class Config {
        public static string APP_PATH {
            get {
                return Application.StartupPath;
            }
        }
        public static string GetPath(string relativePath) {
            return UtilFile.Combine(APP_PATH, relativePath);
        }
    }
}
