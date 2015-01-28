﻿using BetterCms.Core.DataAccess.DataContext.Migrations;
using BetterCms.Core.Models;

using FluentMigrator;

namespace BetterCms.Module.Root.Models.Migrations
{
    [Migration(201501271416)]
    public class Migration201501271416 : DefaultMigration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Migration201501271416"/> class.
        /// </summary>
        public Migration201501271416()
            : base(RootModuleDescriptor.ModuleName)
        {
        }

        public override void Up()
        {
            CreatePageAccessRulesTable();
        }
        private void CreatePageAccessRulesTable()
        {
            Create
                .Table("PageCategories")
                .InSchema(SchemaName)
                .WithColumn("PageId").AsGuid().NotNullable()
                .WithColumn("CategoryId").AsGuid().NotNullable();

            Create.PrimaryKey("PK_Cms_PageCategories").OnTable("PageCategories").WithSchema(SchemaName).Columns(new[] { "PageId", "CategoryId" });

            Create
                .ForeignKey("FK_Cms_PageCategories_PageId_Cms_Page_Id")
                .FromTable("PageAccessRules").InSchema(SchemaName).ForeignColumn("PageId")
                .ToTable("Pages").InSchema(SchemaName).PrimaryColumn("Id");

            Create
                .ForeignKey("FK_Cms_PageCategories_CategoryId_Cms_Category_Id")
                .FromTable("PageCategories").InSchema(SchemaName).ForeignColumn("CategoryId")
                .ToTable("Categories").InSchema(SchemaName).PrimaryColumn("Id");

            Create
                .Index("IX_Cms_PageCategories_PageId")
                .OnTable("PageCategories").InSchema(SchemaName).OnColumn("PageId");

            Create
                .Index("IX_Cms_PageCategories_AccessRuleId")
                .OnTable("PageCategories").InSchema(SchemaName).OnColumn("CategoryId");
        }
    }
}