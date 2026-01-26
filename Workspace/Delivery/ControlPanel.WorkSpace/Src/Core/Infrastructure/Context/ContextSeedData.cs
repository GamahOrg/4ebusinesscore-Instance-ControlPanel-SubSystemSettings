

using _4EPlatform.Domain.Models;

namespace _4EPlatForm.Infrastructure.Context;
public class ContextSeedData
{
    private static DBContext _dbContext;
    private static IServiceProvider _serviceProvider;

    public static void Seed(DBContext appDbContext, IServiceProvider serviceProvider)
    {
        _dbContext = appDbContext;
        _dbContext.Database.EnsureCreated();
        _serviceProvider = serviceProvider;

        // call functions
        SeedSubsytemMainLookup();
        SeedSystemMain();
        SeedWorkSpace();
        SeedSubsystemSettingsMainClassificationLink();





        //SeedComponent();
        //SeedParts();
    }
    private static void SeedWorkSpace()
    {
        if (!_dbContext.SubsytemSettings.Any())
        {

            //string path = System.IO.Path.GetFullPath(@"..\..\");

            string ToolsAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubSystemSeedv3.json");
            string LookUpAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LKNoteTypeSeed.json");
            var workspace = JsonConvert.DeserializeObject<List<SubsytemSettings>>(ToolsAsJson);
            var lookup = JsonConvert.DeserializeObject<List<LK_NoteTypes>>(LookUpAsJson);
            _dbContext.LK_NoteTypes.AddRange(lookup);
            //assignsubsystem(workspace, lookup);
            _dbContext.SubsytemSettings.AddRange(workspace);
            _dbContext.SaveChanges();
            //if (workspace != null)
            //{
            //    foreach (var tool in workspace)
            //    {
            //        tool.CreatedBy = 1;
            //        _dbContext.WorkSpace.Add(tool);
            //        _dbContext.SaveChanges();

            //    }

            //}
        }

        //comment Code 

        else
        {
            string ToolsAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubSystemSeedv3.json");
            var workspace = JsonConvert.DeserializeObject<List<SubsytemSettings>>(ToolsAsJson);
            var subnsystems = _dbContext.SubsytemSettings.ToList();
            var ids = UpdateSubsystem(subnsystems, workspace, null);

            //var listWorkspace = subnsystems.Where(x => !ids.Any(n => n == x.Id) && x.IsExternal == false).ToList();
            //if(listWorkspace.Any())
            //    _dbContext.SubsytemSettings.RemoveRange(listWorkspace);
            //compareubsystem(subnsystems, workspace);
            //workspace.ForEach(n =>
            //{
            //    n.Children.ToList().ForEach(child =>
            //    {

            //        var sub = subnsystems.FirstOrDefault(x => x.Id == n.Id);
            //        if (sub.IsVisiable == null)
            //        {
            //            sub.IsVisiable = n.IsVisiable;
            //            _dbContext.SubsytemSettings.Update(sub);
            //        }
            //        var wrkspace = subnsystems.FirstOrDefault(x => x.Id == child.Id);
            //        if (wrkspace != null)
            //        {
            //            if (wrkspace.IsVisiable == null)
            //            {
            //                wrkspace.IsVisiable = child.IsVisiable;
            //                _dbContext.SubsytemSettings.Update(wrkspace);
            //            }
            //            child.Children?.ToList().ForEach(part =>
            //            {
            //                var subPart = subnsystems.FirstOrDefault(x => x.Id == part.Id);
            //                if (subPart != null && subPart.IsVisiable == null)
            //                {
            //                    subPart.IsVisiable = part.IsVisiable;
            //                    _dbContext.SubsytemSettings.Update(subPart);
            //                }
            //            });
            //            n.Children.Remove(child);
            //        }
            //    });
            //});
            //var s = subnsystems.Select(n => n.Id).ToList();
            //var w = workspace.Where(n => !s.Contains(n.Id)).ToList();


            //_dbContext.SubsytemSettings.AddRange(w);
            _dbContext.SaveChanges();
            var subDelete = subnsystems.Where(s => s.WorkSpaceKey == 34 || s.WorkSpaceKey == 33).ToList();

            if (subDelete.Any())
                _dbContext.SubsytemSettings.RemoveRange(subDelete);
            _dbContext.SaveChanges();
        }
    }

    private static void SeedSystemMain()
    {
        if (!_dbContext.SubsytemSettingsMainClassification.Any())
        {

            //string path = System.IO.Path.GetFullPath(@"..\..\");


            string SystemMainAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubsytemSettingsMainClassification.json");
            var systemmain = JsonConvert.DeserializeObject<List<SubsytemSettingsMainClassification>>(SystemMainAsJson);
            _dbContext.SubsytemSettingsMainClassification.AddRange(systemmain);
            _dbContext.SaveChanges();
        }
        else
        {
            string systemMainAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubsytemSettingsMainClassification.json");
            var systemMainList = JsonConvert.DeserializeObject<List<SubsytemSettingsMainClassification>>(systemMainAsJson);
            var sub = _dbContext.SubsytemSettingsMainClassification.ToList();
            systemMainList.ForEach(n =>
            {
                var main = sub.FirstOrDefault(x => x.Id == n.Id);
                if (main == null)
                    _dbContext.SubsytemSettingsMainClassification.Add(n);
                else
                {
                    if (main.Squence == null)
                    {
                        main.Squence = n.Squence;
                        _dbContext.SubsytemSettingsMainClassification.Update(main);
                    }
                    //if (n.Squence == 0)
                    //{
                    //    sub.ForEach(x =>
                    //    {
                    //        x.Squence = x.Squence + 1;
                    //        _dbContext.SubsytemSettingsMainClassification.Update(x);
                    //    });
                    //    main.Squence = 0;
                    //    _dbContext.SubsytemSettingsMainClassification.Update(main);
                    //}
                    if (main.Icon == null)
                    {
                        main.Icon = n.Icon;
                        _dbContext.SubsytemSettingsMainClassification.Update(main);

                    }
                }
            });
            var ids = sub.Select(x => x.Id).ToList();
            systemMainList = systemMainList.Where(n => !ids.Contains(n.Id)).ToList();

            _dbContext.AddRange(systemMainList);
            // foreach (var item in systemMainList)
            //{
            //    if (!_dbContext.SubsytemSettingsMainClassification.Any(x => x.Id == item.Id)) // Replace UniqueProperty with the actual property
            //    {
            //        _dbContext.SubsytemSettingsMainClassification.Add(item);
            //    }
            //}
            ///
            // var ids = systemMainList.Select(x => x.Id).ToList();
            //var sub2 = sub.Where(n => !ids.Contains(n.Id)).ToList();

            // _dbContext.SubsytemSettingsMainClassification.RemoveRange(sub2);
            _dbContext.SaveChanges();

        }



    }
    private static void SeedSubsytemMainLookup()
    {
        if (!_dbContext.SubsytemMainLookup.Any())
        {
            string SubsytemMainLookupAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubsytemMainLookup.json");
            var SubsytemMainLookup = JsonConvert.DeserializeObject<List<SubsytemMainLookup>>(SubsytemMainLookupAsJson);
            _dbContext.SubsytemMainLookup.AddRange(SubsytemMainLookup);
            _dbContext.SaveChanges();
        }
        else
        {
            string SubsytemMainLookupAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubsytemMainLookup.json");
            var SubsytemMainLookupList = JsonConvert.DeserializeObject<List<SubsytemMainLookup>>(SubsytemMainLookupAsJson);
            var sub = _dbContext.SubsytemMainLookup.ToList();
            SubsytemMainLookupList.ForEach(n =>
            {
                var main = sub.FirstOrDefault(x => x.Id == n.Id);
                if (main == null)
                    _dbContext.SubsytemMainLookup.Add(n);
                else
                {
                    main.NameAr = main.NameAr ?? n.NameAr;
                    main.NameEn = main.NameEn ?? n.NameEn;
                    _dbContext.SubsytemMainLookup.Update(main);
                }
            });

            _dbContext.SaveChanges();
        }



    }
    private static void SeedSubsystemSettingsMainClassificationLink()
    {
        if (!_dbContext.SubsystemSettingsMainClassificationLinks.Any())
        {
            string SubsytemMainLookupAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubsystemSettingsMainClassificationLinkSeed.json");
            var SubsytemMainLookup = JsonConvert.DeserializeObject<List<SubsystemSettingsMainClassificationLink>>(SubsytemMainLookupAsJson);
            _dbContext.SubsystemSettingsMainClassificationLinks.AddRange(SubsytemMainLookup);
            _dbContext.SaveChanges();
        }
        //else
        //{
        //    string SubsytemMainLookupAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "SubsystemSettingsMainClassificationLinkSeed.json");
        //    var SubsytemMainLookup = JsonConvert.DeserializeObject<List<SubsystemSettingsMainClassificationLink>>(SubsytemMainLookupAsJson);
        //    var sub = _dbContext.SubsystemSettingsMainClassificationLinks.ToList();
        //    SubsytemMainLookup.ForEach(n =>
        //    {
        //        var main = sub.FirstOrDefault(x => x.SubsystemSettingsId == n.SubsystemSettingsId && x.SubsytemMainId == n.SubsytemMainId);
        //        if (main == null)
        //            _dbContext.SubsystemSettingsMainClassificationLinks.Add(n);
        //        //else
        //        //{
        //        //    main.SubsytemMainId = main.SubsytemMainId ?? n.SubsytemMainId;
        //        //    main.SubsystemSettingsId = n.SubsystemSettingsId;
        //        //    _dbContext.SubsystemSettingsMainClassificationLinks.Update(main);
        //        //}
        //    });

        //    _dbContext.SaveChanges();
        //}

    }
    //private static void SeedComponent()
    //{
    //    if (!_dbContext.WorkSpace.Where(s => s.ParentId != null && s.NoteTypeId == NoteTypeId.Component).Any())
    //    {

    //        string workspaceJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "ComponentSeed.json");
    //        List<SubsytemSettings> workspace = JsonConvert.DeserializeObject<List<SubsytemSettings>>(workspaceJson);

    //        if (workspace != null)
    //        {
    //            foreach (var tool in workspace)
    //            {
    //                tool.CreatedBy = new Guid();
    //                _dbContext.WorkSpace.Add(tool);
    //                _dbContext.SaveChanges();
    //            }

    //        }
    //    }
    //}
    //private static void SeedParts()
    //{
    //    if (!_dbContext.WorkSpace.Where(s => s.ParentId != null && s.NoteTypeId == NoteTypeId.Part).Any())
    //    {

    //        string workspaceJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "PartsSeed.json");
    //        List<SubsytemSettings> workspace = JsonConvert.DeserializeObject<List<SubsytemSettings>>(workspaceJson);

    //        if (workspace != null)
    //        {
    //            foreach (var tool in workspace)
    //            {
    //                tool.CreatedBy = new Guid();
    //                _dbContext.WorkSpace.Add(tool);
    //                _dbContext.SaveChanges();
    //            }

    //        }
    //    }
    //}
    private static List<SubsytemSettings> assignsubsystem(List<SubsytemSettings> subsystem, List<LK_NoteTypes> lookup)
    {

        subsystem.ForEach(n =>
        {
            n.LKNoteTypes = lookup.FirstOrDefault(x => x.NameEn.Contains(n.LKNoteTypes.NameEn));
            if (n.Children != null)
            {
                assignsubsystem(n.Children.ToList(), lookup);
            }
        });

        return subsystem;
    }
    private static List<Guid> UpdateSubsystem(List<SubsytemSettings> subsystems, List<SubsytemSettings> workspace, Guid? parantId)
    {
        var deletedIds = new List<Guid>();
        workspace.ForEach(s =>
        {
            var sub = subsystems.FirstOrDefault(n => n.Id == s.Id);
            if (sub != null)
            {

                //sub.NoteNameAr = s.NoteNameAr;
                //sub.NoteNameEn = s.NoteNameEn;
              //  sub.IsVisiable = sub.IsVisiable ?? s.IsVisiable;
                sub.ParentId = parantId;
                //sub.SubsytemMainId = s.SubsytemMainId;
                sub.Url = s.Url;
                //sub.Squence = s.Squence;
                sub.CreatedOn = s.CreatedOn;
                sub.UpdatedOn = s.UpdatedOn;
                sub.IsExternal = s.IsExternal ?? false;
                sub.EndPoint = sub.EndPoint == null || sub.EndPoint == " " || sub.EndPoint == "" ? s.EndPoint : sub.EndPoint;
               // sub.SubsytemMainLookupId = sub.SubsytemMainLookupId ?? s.SubsytemMainLookupId;
                sub.WorkSpaceKey = s.WorkSpaceKey;
                sub.NoteTypeId = sub.NoteTypeId ?? s.NoteTypeId;
                sub.Description = sub.Description ?? "";
                sub.IsUpdated = s.IsUpdated;
                sub.DescriptionEn = string.IsNullOrEmpty(sub.DescriptionEn) == true ? (s.DescriptionEn ?? "") : sub.DescriptionEn;
                _dbContext.SubsytemSettings.Update(sub);
            }
            else
            {
                var wrk = new SubsytemSettings
                {
                    Id = s.Id,
                    NoteNameEn = s.NoteNameEn,
                    Description = s.Description,
                    DescriptionEn = s.DescriptionEn ?? "",
                    Color = s.Color,
                    Icon = s.Icon,
                    IsActive = s.IsActive,
                    IsVisiable = s.IsVisiable ?? s.IsVisiable,
                    NoteNameAr = s.NoteNameAr,
                    NoteTypeId = s.NoteTypeId,
                    OrganizationHierarchyId = s.OrganizationHierarchyId,
                    Url = s.Url,
                    CreatedBy = new Guid(),
                    VisitDate = DateTime.Now,
                    ParentId = parantId,
                    CreatedOn = DateTime.Now,
                    State = s.State,
                    WorkSpaceKey = s.WorkSpaceKey,
                    // SubsytemMainId = s.SubsytemMainId,
                    SubsytemMainLookupId = s.SubsytemMainLookupId,
                    IsExternal = s.IsExternal,
                    IsSinglSignOn = s.IsSinglSignOn,
                    Squence = subsystems.Max(x=>x.Squence) + 1,
                    EndPoint=s.EndPoint,
                    IsUpdated = s.IsUpdated
                };
                _dbContext.SubsytemSettings.Add(wrk);
            }
            deletedIds.Add(s.Id);
            if (s.Children != null) {
                var ids = UpdateSubsystem(subsystems, s.Children.ToList(), s.Id);
                deletedIds.AddRange(ids);
            }
        });
        return deletedIds;
        //_dbContext.SubsytemSettings.RemoveRange(_dbContext.SubsytemSettings.ToList());
        //_dbContext.LK_NoteTypes.RemoveRange(_dbContext.LK_NoteTypes.ToList());
        //_dbContext.SaveChanges();
        //_dbContext.LK_NoteTypes.AddRange(lookup);
        //_dbContext.SubsytemSettings.AddRange(workspace);

    }
    private static void compareubsystem(List<SubsytemSettings> subsystems, List<SubsytemSettings> workspace)
    {
        string LookUpAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LKNoteTypeSeed.json");
        var lookup = JsonConvert.DeserializeObject<List<LK_NoteTypes>>(LookUpAsJson);



        workspace.ForEach(s =>
        {
            var sub = subsystems.FirstOrDefault(n => n.NoteNameEn.ToLower().Contains(s.NoteNameEn.ToLower()) && n.ParentId == null); ;
            if (sub != null)
                s.Id = sub.Id;

            if (s.Children != null)
                compareubsystem2(subsystems, s.Children.ToList());
        });

        _dbContext.SubsytemSettings.RemoveRange(_dbContext.SubsytemSettings.ToList());
        _dbContext.LK_NoteTypes.RemoveRange(_dbContext.LK_NoteTypes.ToList());
        _dbContext.SaveChanges();
        _dbContext.LK_NoteTypes.AddRange(lookup);
        _dbContext.SubsytemSettings.AddRange(workspace);

    }
    private static void compareubsystem2(List<SubsytemSettings> subsystems, List<SubsytemSettings> workspace)
    {

        workspace.ForEach(s =>
        {
            var sub = subsystems.FirstOrDefault(n => n.WorkSpaceKey == s.WorkSpaceKey);
            if (sub != null)
                s.Id = sub.Id;

            if (s.Children != null)
                compareubsystem2(subsystems, s.Children.ToList());
        });
    }
    public class SubsytemSettingsComparer : IEqualityComparer<SubsytemSettings>
    {
        public bool Equals(SubsytemSettings x, SubsytemSettings y)
        {

            return x.WorkSpaceKey == y.WorkSpaceKey;
        }

        public int GetHashCode(SubsytemSettings obj)
        {

            return HashCode.Combine(obj.WorkSpaceKey);
        }
    }
}