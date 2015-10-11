using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CS431_Project;

namespace CS431_Project.Migrations
{
    [DbContext(typeof(InterestContext))]
    partial class firstmigration
    {
        public override string Id
        {
            get { return "20151011070658_firstmigration"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540");

            modelBuilder.Entity("CS431_Project.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<int?>("Age")
                        .Annotation("Relational:ColumnName", "age");

                    b.Property<string>("Name")
                        .Annotation("Relational:ColumnName", "name");

                    b.Property<int?>("otherthingthingId");

                    b.Key("InterestId");

                    b.Annotation("Relational:TableName", "interest");
                });

            modelBuilder.Entity("CS431_Project.thing", b =>
                {
                    b.Property<int>("thingId")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "id");

                    b.Property<int?>("Value")
                        .Annotation("Relational:ColumnName", "value");

                    b.Key("thingId");
                });

            modelBuilder.Entity("CS431_Project.Interest", b =>
                {
                    b.Reference("CS431_Project.thing")
                        .InverseCollection()
                        .ForeignKey("otherthingthingId");
                });
        }
    }
}
