using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace eTellerKeeper.Aspect
{
    static class Weave
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Trace(string tag, Action action)
        {
            try
            {
                _logger.Trace($"BEGIN : {tag}");
                action();
                _logger.Info($"SUCCESS : {tag}");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, $"EXCEPTION : {tag} : {ex.Message}");
                throw ex;
            }
            finally
            {
                _logger.Trace($"END : {tag}");
            }
        }

        public static void Trace(string tag, Func<object> result, Action action)
        {
            try
            {
                _logger.Trace($"BEGIN : {tag}");
                action();

                var resultObject = result();
                string resultMessage = resultObject == null ? "Null" : resultObject.ToString();
                _logger.Info($"SUCCESS : {tag} → {resultMessage}");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, $"EXCEPTION : {tag} : {ex.Message}");
                throw ex;
            }
            finally
            {
                _logger.Trace($"END : {tag}");
            }
        }

        public static void Retry(int retryTimes, int interval, Action action) {
            throw new ApplicationException("未實作 Retry");
        }
    }
}