using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.TestSuite.Sample
{
    [TestSuiteAttribute(TestCaseType.Performance)]
    public class PerformanceTestCaseTagMetadataEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            ITagDataContext dataContext = DataContextFactory.CreateTagDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip test cases for TagMetadataEndpoint, because of missing data context", LogType.Info);
                return true;
            }

            ITagMetadataEndpoint tester = EndpointFactory.CreateTagMetadataEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateTagMetadataEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;

            if (File.Exists(dataContext.GetTagDataFile()))
            {
                OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata - PerformanceTest");
                try
                {
                    TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest(dataContext);
                    requestTagMetaData.tagIds.Clear();
                    TagMetaDataResponse responseTagMetaData;
                    Stopwatch watch = new Stopwatch();
                    int tagCount = 0;
                    TagMetaDataActionResult actionResult;

                    // load data
                    XDocument doc;
                    doc = XDocument.Load(dataContext.GetTagDataFile());
                    foreach (var tag in doc.Descendants("tag"))
                    {

                        var tagId = tag.Attribute("id");
                        if (null == tagId)
                            continue;

                        requestTagMetaData.tagIds.Add(tagId.Value);

                        // validate
                        if (requestTagMetaData.tagIds.Count == 1000)
                        {
                            tagCount += requestTagMetaData.tagIds.Count;
                            watch.Start();
                            actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
                            result = result && (actionResult == TagMetaDataActionResult.Successful);
                            watch.Stop();
                            Logger.AddLog(requestTagMetaData.tagIds.Count + " tags checked. Result:" + actionResult + "  Required time:" + watch.Elapsed);
                            requestTagMetaData.tagIds.Clear();
                            watch.Reset();
                        }
                    }

                    // validate rest
                    if (requestTagMetaData.tagIds.Count > 0)
                    {
                        tagCount += requestTagMetaData.tagIds.Count;
                        watch.Start();
                        actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
                        result = result && (actionResult == TagMetaDataActionResult.Successful);
                        watch.Stop();
                        Logger.AddLog(requestTagMetaData.tagIds.Count + " tags checked. Result:" + result + "  Required time:" + watch.Elapsed);
                        requestTagMetaData.tagIds.Clear();
                        watch.Reset();
                    }

                    Logger.AddLog("Test count: " + tagCount);
                }
                catch (Exception e)
                {
                    Logger.AddLog(e.Message, LogType.Error);
                }

                OnAfterTest();
            }

            return result; 
        }
    }
}
