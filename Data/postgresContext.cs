﻿using System;
using System.Collections.Generic;
using fag_el_gamous.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace fag_el_gamous.Data
{
    public partial class postgresContext : DbContext
    {
        
        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Analysis> Analyses { get; set; } = null!;
        public virtual DbSet<AnalysisTextile> AnalysisTextiles { get; set; } = null!;
        public virtual DbSet<Artifactfagelgamousregister> Artifactfagelgamousregisters { get; set; } = null!;
        public virtual DbSet<ArtifactfagelgamousregisterBurialmain> ArtifactfagelgamousregisterBurialmains { get; set; } = null!;
        public virtual DbSet<Artifactkomaushimregister> Artifactkomaushimregisters { get; set; } = null!;
        public virtual DbSet<ArtifactkomaushimregisterBurialmain> ArtifactkomaushimregisterBurialmains { get; set; } = null!;
        public virtual DbSet<Biological> Biologicals { get; set; } = null!;
        public virtual DbSet<BiologicalC14> BiologicalC14s { get; set; } = null!;
        public virtual DbSet<Bodyanalysischart> Bodyanalysischarts { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Burialmain> Burialmains { get; set; } = null!;
        public virtual DbSet<BurialmainBiological> BurialmainBiologicals { get; set; } = null!;
        public virtual DbSet<BurialmainBodyanalysischart> BurialmainBodyanalysischarts { get; set; } = null!;
        public virtual DbSet<BurialmainCranium> BurialmainCrania { get; set; } = null!;
        public virtual DbSet<BurialmainTextile> BurialmainTextiles { get; set; } = null!;
        public virtual DbSet<C14> C14s { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<ColorTextile> ColorTextiles { get; set; } = null!;
        public virtual DbSet<Cranium> Crania { get; set; } = null!;
        public virtual DbSet<Decoration> Decorations { get; set; } = null!;
        public virtual DbSet<DecorationTextile> DecorationTextiles { get; set; } = null!;
        public virtual DbSet<Detailscleaneddatum> Detailscleaneddata { get; set; } = null!;
        public virtual DbSet<Dimension> Dimensions { get; set; } = null!;
        public virtual DbSet<DimensionTextile> DimensionTextiles { get; set; } = null!;
        public virtual DbSet<Newsarticle> Newsarticles { get; set; } = null!;
        public virtual DbSet<PhotodataTextile> PhotodataTextiles { get; set; } = null!;
        public virtual DbSet<Photodatum> Photodata { get; set; } = null!;
        public virtual DbSet<Photoform> Photoforms { get; set; } = null!;
        public virtual DbSet<Structure> Structures { get; set; } = null!;
        public virtual DbSet<StructureTextile> StructureTextiles { get; set; } = null!;
        public virtual DbSet<Teammember> Teammembers { get; set; } = null!;
        public virtual DbSet<Textile> Textiles { get; set; } = null!;
        public virtual DbSet<Textilefunction> Textilefunctions { get; set; } = null!;
        public virtual DbSet<TextilefunctionTextile> TextilefunctionTextiles { get; set; } = null!;
        public virtual DbSet<Yarnmanipulation> Yarnmanipulations { get; set; } = null!;
        public virtual DbSet<YarnmanipulationTextile> YarnmanipulationTextiles { get; set; } = null!;
        public virtual DbSet<BodyanalysischartBurialmain> BodyanalysischartBurialmains { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=intex-database-1.cddaeznphjkz.us-east-1.rds.amazonaws.com; Database=postgres; Username=postgres; Password=intex-rds-postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.ToTable("analysis");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Analysisid).HasColumnName("analysisid");

                entity.Property(e => e.Analysistype).HasColumnName("analysistype");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date");

                entity.Property(e => e.Doneby)
                    .HasMaxLength(100)
                    .HasColumnName("doneby");
            });

            modelBuilder.Entity<AnalysisTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainAnalysisid, e.MainTextileid })
                    .HasName("main$analysis_textile_pkey");

                entity.ToTable("analysis_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainAnalysisid }, "idx_main$analysis_textile_main$textile_main$analysis");

                entity.Property(e => e.MainAnalysisid).HasColumnName("main$analysisid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Analysis)
                    .WithMany(p => p.AnalysisTextiles)
                    .HasForeignKey(d => d.MainAnalysisid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$analysis_textile_main$analysis");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.AnalysisTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$analysis_textile_main$textile");
            });

            

            modelBuilder.Entity<Artifactfagelgamousregister>(entity =>
            {
                entity.ToTable("artifactfagelgamousregister");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Coordinateset)
                    .HasMaxLength(200)
                    .HasColumnName("coordinateset");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Photographed)
                    .HasMaxLength(3)
                    .HasColumnName("photographed");

                entity.Property(e => e.Registernum)
                    .HasMaxLength(30)
                    .HasColumnName("registernum");
            });

            modelBuilder.Entity<ArtifactfagelgamousregisterBurialmain>(entity =>
            {
                entity.HasKey(e => new { e.MainArtifactfagelgamousregisterid, e.MainBurialmainid })
                    .HasName("main$artifactfagelgamousregister_burialmain_pkey");

                entity.ToTable("artifactfagelgamousregister_burialmain");

                entity.HasIndex(e => new { e.MainBurialmainid, e.MainArtifactfagelgamousregisterid }, "idx_main$artifactfagelgamousregister_burialmain");

                entity.Property(e => e.MainArtifactfagelgamousregisterid).HasColumnName("main$artifactfagelgamousregisterid");

                entity.Property(e => e.MainBurialmainid).HasColumnName("main$burialmainid");
            });

            modelBuilder.Entity<Artifactkomaushimregister>(entity =>
            {
                entity.ToTable("artifactkomaushimregister");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Currentlocation)
                    .HasMaxLength(200)
                    .HasColumnName("currentlocation");

                entity.Property(e => e.Date)
                    .HasMaxLength(200)
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Dimensions)
                    .HasMaxLength(200)
                    .HasColumnName("dimensions");

                entity.Property(e => e.Entrydate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("entrydate");

                entity.Property(e => e.Excavatornum)
                    .HasMaxLength(200)
                    .HasColumnName("excavatornum");

                entity.Property(e => e.Finder)
                    .HasMaxLength(200)
                    .HasColumnName("finder");

                entity.Property(e => e.Material)
                    .HasMaxLength(200)
                    .HasColumnName("material");

                entity.Property(e => e.Number)
                    .HasMaxLength(200)
                    .HasColumnName("number");

                entity.Property(e => e.Photos)
                    .HasMaxLength(3)
                    .HasColumnName("photos");

                entity.Property(e => e.Position)
                    .HasMaxLength(200)
                    .HasColumnName("position");

                entity.Property(e => e.Provenance)
                    .HasMaxLength(200)
                    .HasColumnName("provenance");

                entity.Property(e => e.Qualityphotos)
                    .HasMaxLength(3)
                    .HasColumnName("qualityphotos");

                entity.Property(e => e.Rehousedto)
                    .HasMaxLength(200)
                    .HasColumnName("rehousedto");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .HasColumnName("remarks");
            });

            modelBuilder.Entity<ArtifactkomaushimregisterBurialmain>(entity =>
            {
                entity.HasKey(e => new { e.MainArtifactkomaushimregisterid, e.MainBurialmainid })
                    .HasName("main$artifactqumoshimregistrar_burialmain_pkey");

                entity.ToTable("artifactkomaushimregister_burialmain");

                entity.HasIndex(e => new { e.MainBurialmainid, e.MainArtifactkomaushimregisterid }, "idx_main$artifactkomaushimregister_burialmain");

                entity.Property(e => e.MainArtifactkomaushimregisterid).HasColumnName("main$artifactkomaushimregisterid");

                entity.Property(e => e.MainBurialmainid).HasColumnName("main$burialmainid");
            });

            modelBuilder.Entity<Biological>(entity =>
            {
                entity.ToTable("biological");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Bagnumber).HasColumnName("bagnumber");

                entity.Property(e => e.Clusternumber).HasColumnName("clusternumber");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date");

                entity.Property(e => e.Initials)
                    .HasMaxLength(200)
                    .HasColumnName("initials");

                entity.Property(e => e.Notes)
                    .HasMaxLength(2000)
                    .HasColumnName("notes");

                entity.Property(e => e.Previouslysampled)
                    .HasMaxLength(200)
                    .HasColumnName("previouslysampled");

                entity.Property(e => e.Racknumber).HasColumnName("racknumber");
            });

            modelBuilder.Entity<BiologicalC14>(entity =>
            {
                entity.HasKey(e => new { e.MainBiologicalid, e.MainC14id })
                    .HasName("main$biological_c14_pkey");

                entity.ToTable("biological_c14");

                entity.HasIndex(e => new { e.MainC14id, e.MainBiologicalid }, "idx_main$biological_c14_main$c14_main$biological");

                entity.Property(e => e.MainBiologicalid).HasColumnName("main$biologicalid");

                entity.Property(e => e.MainC14id).HasColumnName("main$c14id");
            });

            modelBuilder.Entity<Bodyanalysischart>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("bodyanalysischart_pkey");

                entity.ToTable("bodyanalysischart");

                entity.Property(e => e.Key)
                    .ValueGeneratedNever()
                    .HasColumnName("key");

                entity.Property(e => e.Area)
                    .HasMaxLength(2)
                    .HasColumnName("area");

                entity.Property(e => e.Burialnumber).HasColumnName("burialnumber");

                entity.Property(e => e.CariesPeriodontalDisease)
                    .HasMaxLength(102)
                    .HasColumnName("caries_periodontal_disease");

                entity.Property(e => e.Dateofexamination)
                    .HasMaxLength(25)
                    .HasColumnName("dateofexamination");

                entity.Property(e => e.DorsalpittingBoolean)
                    .HasMaxLength(6)
                    .HasColumnName("dorsalpitting_boolean");

                entity.Property(e => e.Eastwest)
                    .HasMaxLength(1)
                    .HasColumnName("eastwest");

                entity.Property(e => e.Estimatestature)
                    .HasMaxLength(30)
                    .HasColumnName("estimatestature");

                entity.Property(e => e.Femur)
                    .HasMaxLength(10)
                    .HasColumnName("femur");

                entity.Property(e => e.Femurheaddiameter)
                    .HasMaxLength(5)
                    .HasColumnName("femurheaddiameter");

                entity.Property(e => e.Femurlength)
                    .HasPrecision(4, 1)
                    .HasColumnName("femurlength");

                entity.Property(e => e.Gonion)
                    .HasMaxLength(7)
                    .HasColumnName("gonion");

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(86)
                    .HasColumnName("haircolor");

                entity.Property(e => e.Humerus)
                    .HasMaxLength(10)
                    .HasColumnName("humerus");

                entity.Property(e => e.Humerusheaddiameter)
                    .HasMaxLength(5)
                    .HasColumnName("humerusheaddiameter");

                entity.Property(e => e.Humeruslength)
                    .HasPrecision(4, 1)
                    .HasColumnName("humeruslength");

                entity.Property(e => e.Lamboidsuture)
                    .HasMaxLength(17)
                    .HasColumnName("lamboidsuture");

                entity.Property(e => e.MedialIpRamus)
                    .HasMaxLength(6)
                    .HasColumnName("medial_ip_ramus");

                entity.Property(e => e.Northsouth)
                    .HasMaxLength(1)
                    .HasColumnName("northsouth");

                entity.Property(e => e.Notes)
                    .HasMaxLength(859)
                    .HasColumnName("notes");

                entity.Property(e => e.Nuchalcrest)
                    .HasMaxLength(7)
                    .HasColumnName("nuchalcrest");

                entity.Property(e => e.Observations)
                    .HasMaxLength(830)
                    .HasColumnName("observations");

                entity.Property(e => e.Orbitedge)
                    .HasMaxLength(6)
                    .HasColumnName("orbitedge");

                entity.Property(e => e.Osteophytosis)
                    .HasMaxLength(6)
                    .HasColumnName("osteophytosis");

                entity.Property(e => e.Parietalbossing)
                    .HasMaxLength(25)
                    .HasColumnName("parietalbossing");

                entity.Property(e => e.PreauricularsulcusBoolean)
                    .HasMaxLength(1)
                    .HasColumnName("preauricularsulcus_boolean");

                entity.Property(e => e.Preservationindex)
                    .HasPrecision(4, 2)
                    .HasColumnName("preservationindex");

                entity.Property(e => e.Pubicbone)
                    .HasMaxLength(6)
                    .HasColumnName("pubicbone");

                entity.Property(e => e.Robust)
                    .HasMaxLength(9)
                    .HasColumnName("robust");

                entity.Property(e => e.Sciaticnotch)
                    .HasMaxLength(6)
                    .HasColumnName("sciaticnotch");

                entity.Property(e => e.Sphenooccipitalsynchrondrosis)
                    .HasMaxLength(10)
                    .HasColumnName("sphenooccipitalsynchrondrosis");

                entity.Property(e => e.Squamossuture)
                    .HasMaxLength(10)
                    .HasColumnName("squamossuture");

                entity.Property(e => e.Squareeastwest).HasColumnName("squareeastwest");

                entity.Property(e => e.Squarenorthsouth).HasColumnName("squarenorthsouth");

                entity.Property(e => e.Subpubicangle)
                    .HasMaxLength(6)
                    .HasColumnName("subpubicangle");

                entity.Property(e => e.Supraorbitalridges)
                    .HasMaxLength(6)
                    .HasColumnName("supraorbitalridges");

                entity.Property(e => e.Toothattrition)
                    .HasMaxLength(8)
                    .HasColumnName("toothattrition");

                entity.Property(e => e.Tootheruption)
                    .HasMaxLength(184)
                    .HasColumnName("tootheruption");

                entity.Property(e => e.Tootheruptionageestimate)
                    .HasMaxLength(19)
                    .HasColumnName("tootheruptionageestimate");

                entity.Property(e => e.Ventralarc)
                    .HasMaxLength(3)
                    .HasColumnName("ventralarc");

                entity.Property(e => e.Zygomaticcrest)
                    .HasMaxLength(7)
                    .HasColumnName("zygomaticcrest");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasMaxLength(200)
                    .HasColumnName("author");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Link)
                    .HasMaxLength(300)
                    .HasColumnName("link");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Burialmain>(entity =>
            {
                entity.ToTable("burialmain");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Adultsubadult)
                    .HasMaxLength(200)
                    .HasColumnName("adultsubadult");

                entity.Property(e => e.Ageatdeath)
                    .HasMaxLength(200)
                    .HasColumnName("ageatdeath");

                entity.Property(e => e.Area)
                    .HasMaxLength(200)
                    .HasColumnName("area");

                entity.Property(e => e.Burialid).HasColumnName("burialid");

                entity.Property(e => e.Burialmaterials)
                    .HasMaxLength(5)
                    .HasColumnName("burialmaterials");

                entity.Property(e => e.Burialnumber)
                    .HasMaxLength(200)
                    .HasColumnName("burialnumber");

                entity.Property(e => e.Clusternumber)
                    .HasMaxLength(200)
                    .HasColumnName("clusternumber");

                entity.Property(e => e.Dataexpertinitials)
                    .HasMaxLength(200)
                    .HasColumnName("dataexpertinitials");

                entity.Property(e => e.Dateofexcavation)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dateofexcavation");

                entity.Property(e => e.Depth)
                    .HasMaxLength(200)
                    .HasColumnName("depth");

                entity.Property(e => e.Eastwest)
                    .HasMaxLength(200)
                    .HasColumnName("eastwest");

                entity.Property(e => e.Excavationrecorder)
                    .HasMaxLength(100)
                    .HasColumnName("excavationrecorder");

                entity.Property(e => e.Facebundles)
                    .HasMaxLength(200)
                    .HasColumnName("facebundles");

                entity.Property(e => e.Fieldbookexcavationyear)
                    .HasMaxLength(200)
                    .HasColumnName("fieldbookexcavationyear");

                entity.Property(e => e.Fieldbookpage)
                    .HasMaxLength(200)
                    .HasColumnName("fieldbookpage");

                entity.Property(e => e.Goods)
                    .HasMaxLength(200)
                    .HasColumnName("goods");

                entity.Property(e => e.Hair)
                    .HasMaxLength(5)
                    .HasColumnName("hair");

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(200)
                    .HasColumnName("haircolor");

                entity.Property(e => e.Headdirection)
                    .HasMaxLength(200)
                    .HasColumnName("headdirection");

                entity.Property(e => e.Length)
                    .HasMaxLength(200)
                    .HasColumnName("length");

                entity.Property(e => e.Northsouth)
                    .HasMaxLength(200)
                    .HasColumnName("northsouth");

                entity.Property(e => e.Photos)
                    .HasMaxLength(5)
                    .HasColumnName("photos");

                entity.Property(e => e.Preservation)
                    .HasMaxLength(200)
                    .HasColumnName("preservation");

                entity.Property(e => e.Samplescollected)
                    .HasMaxLength(200)
                    .HasColumnName("samplescollected");

                entity.Property(e => e.Sex)
                    .HasMaxLength(200)
                    .HasColumnName("sex");

                entity.Property(e => e.Shaftnumber)
                    .HasMaxLength(200)
                    .HasColumnName("shaftnumber");

                entity.Property(e => e.Southtofeet)
                    .HasMaxLength(200)
                    .HasColumnName("southtofeet");

                entity.Property(e => e.Southtohead)
                    .HasMaxLength(200)
                    .HasColumnName("southtohead");

                entity.Property(e => e.Squareeastwest)
                    .HasMaxLength(200)
                    .HasColumnName("squareeastwest");

                entity.Property(e => e.Squarenorthsouth)
                    .HasMaxLength(200)
                    .HasColumnName("squarenorthsouth");

                entity.Property(e => e.Text)
                    .HasMaxLength(2000)
                    .HasColumnName("text");

                entity.Property(e => e.Westtofeet)
                    .HasMaxLength(200)
                    .HasColumnName("westtofeet");

                entity.Property(e => e.Westtohead)
                    .HasMaxLength(200)
                    .HasColumnName("westtohead");

                entity.Property(e => e.Wrapping)
                    .HasMaxLength(200)
                    .HasColumnName("wrapping");
            });

            modelBuilder.Entity<BurialmainBiological>(entity =>
            {
                entity.HasKey(e => new { e.MainBurialmainid, e.MainBiologicalid })
                    .HasName("main$burialmain_biological_pkey");

                entity.ToTable("burialmain_biological");

                entity.HasIndex(e => new { e.MainBiologicalid, e.MainBurialmainid }, "idx_main$burialmain_biological_main$biological_main$burialmain");

                entity.Property(e => e.MainBurialmainid).HasColumnName("main$burialmainid");

                entity.Property(e => e.MainBiologicalid).HasColumnName("main$biologicalid");
            });

            modelBuilder.Entity<BurialmainBodyanalysischart>(entity =>
            {
                entity.HasKey(e => new { e.MainBurialmainid, e.MainBodyanalysischartid })
                    .HasName("main$burialmain_bodyanalysischart_pkey");

                entity.ToTable("burialmain_bodyanalysischart");

                entity.HasIndex(e => new { e.MainBodyanalysischartid, e.MainBurialmainid }, "idx_main$burialmain_bodyanalysischart");

                entity.Property(e => e.MainBurialmainid).HasColumnName("main$burialmainid");

                entity.Property(e => e.MainBodyanalysischartid).HasColumnName("main$bodyanalysischartid");
            });

            modelBuilder.Entity<BurialmainCranium>(entity =>
            {
                entity.HasKey(e => new { e.MainBurialmainid, e.MainCraniumid })
                    .HasName("main$burialmain_cranium_pkey");

                entity.ToTable("burialmain_cranium");

                entity.HasIndex(e => new { e.MainCraniumid, e.MainBurialmainid }, "idx_main$burialmain_cranium_main$cranium_main$burialmain");

                entity.Property(e => e.MainBurialmainid).HasColumnName("main$burialmainid");

                entity.Property(e => e.MainCraniumid).HasColumnName("main$craniumid");
            });

            modelBuilder.Entity<BurialmainTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainBurialmainid, e.MainTextileid })
                    .HasName("main$burialmain_textile_pkey");

                entity.ToTable("burialmain_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainBurialmainid }, "idx_main$burialmain_textile_main$textile_main$burialmain");

                entity.Property(e => e.MainBurialmainid).HasColumnName("main$burialmainid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(e => e.Burialmain)
                      .WithMany(e => e.BurialmainTextiles)
                      .HasForeignKey(e => e.MainBurialmainid);

                entity.HasOne(e => e.Textile)
                      .WithMany(e => e.BurialmainTextiles)
                      .HasForeignKey(e => e.MainTextileid);
            });

        modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("color");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Colorid).HasColumnName("colorid");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<ColorTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainColorid, e.MainTextileid })
                    .HasName("main$color_textile_pkey");

                entity.ToTable("color_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainColorid }, "idx_main$color_textile_main$textile_main$color");

                entity.Property(e => e.MainColorid).HasColumnName("main$colorid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ColorTextiles)
                    .HasForeignKey(d => d.MainColorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$color_textile_main$color");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.ColorTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$color_textile_main$textile");
            });

            modelBuilder.Entity<Cranium>(entity =>
            {
                entity.ToTable("cranium");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AlphaCore)
                    .HasPrecision(28, 8)
                    .HasColumnName("alpha_core");

                entity.Property(e => e.BasionBregmaHeight)
                    .HasPrecision(28, 8)
                    .HasColumnName("basion_bregma_height");

                entity.Property(e => e.BasionNasion)
                    .HasPrecision(28, 8)
                    .HasColumnName("basion_nasion");

                entity.Property(e => e.BasionProsthionLength)
                    .HasPrecision(28, 8)
                    .HasColumnName("basion_prosthion_length");

                entity.Property(e => e.BizygomaticDiameter)
                    .HasPrecision(28, 8)
                    .HasColumnName("bizygomatic_diameter");

                entity.Property(e => e.InterobitalBreadth)
                    .HasPrecision(28, 8)
                    .HasColumnName("interobital_breadth");

                entity.Property(e => e.InterorbitalBreadth)
                    .HasPrecision(28, 8)
                    .HasColumnName("interorbital_breadth");

                entity.Property(e => e.InterorbitalSubtense)
                    .HasPrecision(28, 8)
                    .HasColumnName("interorbital_subtense");

                entity.Property(e => e.MaxNasalBreadth)
                    .HasPrecision(28, 8)
                    .HasColumnName("max_nasal_breadth");

                entity.Property(e => e.Maxcraniumbreadth)
                    .HasPrecision(28, 8)
                    .HasColumnName("maxcraniumbreadth");

                entity.Property(e => e.Maxcraniumlength)
                    .HasPrecision(28, 8)
                    .HasColumnName("maxcraniumlength");

                entity.Property(e => e.MidOrbitalBreadth)
                    .HasPrecision(28, 8)
                    .HasColumnName("mid_orbital_breadth");

                entity.Property(e => e.MidOrbitalSubtense)
                    .HasPrecision(28, 8)
                    .HasColumnName("mid_orbital_subtense");

                entity.Property(e => e.NasionProsthionUpper)
                    .HasPrecision(28, 8)
                    .HasColumnName("nasion_prosthion_upper");

                entity.Property(e => e.NasoAlphaSubtense)
                    .HasPrecision(28, 8)
                    .HasColumnName("naso_alpha__subtense");
            });

            modelBuilder.Entity<Decoration>(entity =>
            {
                entity.ToTable("decoration");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Decorationid).HasColumnName("decorationid");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<DecorationTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainDecorationid, e.MainTextileid })
                    .HasName("main$decoration_textile_pkey");

                entity.ToTable("decoration_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainDecorationid }, "idx_main$decoration_textile_main$textile_main$decoration");

                entity.Property(e => e.MainDecorationid).HasColumnName("main$decorationid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Decoration)
                    .WithMany(p => p.DecorationTextiles)
                    .HasForeignKey(d => d.MainDecorationid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$decoration_textile_main$decoration");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.DecorationTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$decoration_textile_main$textile");
            });

            modelBuilder.Entity<Dimension>(entity =>
            {
                entity.ToTable("dimension");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Dimensionid).HasColumnName("dimensionid");

                entity.Property(e => e.Dimensiontype)
                    .HasMaxLength(500)
                    .HasColumnName("dimensiontype");

                entity.Property(e => e.Value)
                    .HasMaxLength(200)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<DimensionTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainDimensionid, e.MainTextileid })
                    .HasName("main$dimension_textile_pkey");

                entity.ToTable("dimension_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainDimensionid }, "idx_main$dimension_textile_main$textile_main$dimension");

                entity.Property(e => e.MainDimensionid).HasColumnName("main$dimensionid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Dimension)
                    .WithMany(p => p.DimensionTextiles)
                    .HasForeignKey(d => d.MainDimensionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$dimension_textile_main$dimension");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.DimensionTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$dimension_textile_main$textile");
            });

            modelBuilder.Entity<Newsarticle>(entity =>
            {
                entity.ToTable("newsarticle");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasColumnType("character varying")
                    .HasColumnName("author");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasColumnType("character varying")
                    .HasColumnName("url");

                entity.Property(e => e.Urltoimage)
                    .HasColumnType("character varying")
                    .HasColumnName("urltoimage");
            });

            modelBuilder.Entity<PhotodataTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainPhotodataid, e.MainTextileid })
                    .HasName("main$photodata_textile_pkey");

                entity.ToTable("photodata_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainPhotodataid }, "idx_main$photodata_textile_main$textile_main$photodata");

                entity.Property(e => e.MainPhotodataid).HasColumnName("main$photodataid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Photodatum)
                    .WithMany(p => p.photodataTextiles)
                    .HasForeignKey(d => d.MainPhotodataid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$photodata_textile_main$photodata");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.photodataTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$photodata_textile_main$textile");
            });

            modelBuilder.Entity<Photodatum>(entity =>
            {
                entity.ToTable("photodata");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Filename)
                    .HasMaxLength(200)
                    .HasColumnName("filename");

                entity.Property(e => e.Photodataid).HasColumnName("photodataid");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<Photoform>(entity =>
            {
                entity.ToTable("photoform");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Area)
                    .HasMaxLength(100)
                    .HasColumnName("area");

                entity.Property(e => e.Burialnumber)
                    .HasMaxLength(10)
                    .HasColumnName("burialnumber");

                entity.Property(e => e.Eastwest)
                    .HasMaxLength(1)
                    .HasColumnName("eastwest");

                entity.Property(e => e.Filename)
                    .HasMaxLength(200)
                    .HasColumnName("filename");

                entity.Property(e => e.Northsouth)
                    .HasMaxLength(1)
                    .HasColumnName("northsouth");

                entity.Property(e => e.Photodate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("photodate");

                entity.Property(e => e.Photographer)
                    .HasMaxLength(100)
                    .HasColumnName("photographer");

                entity.Property(e => e.Squareeastwest)
                    .HasMaxLength(100)
                    .HasColumnName("squareeastwest");

                entity.Property(e => e.Squarenorthsouth)
                    .HasMaxLength(5)
                    .HasColumnName("squarenorthsouth");

                entity.Property(e => e.Tableassociation)
                    .HasMaxLength(10)
                    .HasColumnName("tableassociation");
            });

            modelBuilder.Entity<Structure>(entity =>
            {
                entity.ToTable("structure");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Structureid).HasColumnName("structureid");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<StructureTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainStructureid, e.MainTextileid })
                    .HasName("main$structure_textile_pkey");

                entity.ToTable("structure_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainStructureid }, "idx_main$structure_textile_main$textile_main$structure");

                entity.Property(e => e.MainStructureid).HasColumnName("main$structureid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Structure)
                    .WithMany(p => p.StructureTextiles)
                    .HasForeignKey(d => d.MainStructureid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$structure_textile_main$structure");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.StructureTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$structure_textile_main$textile");
            });

            modelBuilder.Entity<Teammember>(entity =>
            {
                entity.ToTable("teammember");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Bio)
                    .HasColumnType("character varying")
                    .HasColumnName("bio");

                entity.Property(e => e.Membername)
                    .HasMaxLength(200)
                    .HasColumnName("membername");
            });

            modelBuilder.Entity<Textile>(entity =>
            {
                entity.ToTable("textile");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Burialnumber)
                    .HasMaxLength(200)
                    .HasColumnName("burialnumber");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Direction)
                    .HasMaxLength(200)
                    .HasColumnName("direction");

                entity.Property(e => e.Estimatedperiod)
                    .HasMaxLength(200)
                    .HasColumnName("estimatedperiod");

                entity.Property(e => e.Locale)
                    .HasMaxLength(200)
                    .HasColumnName("locale");

                entity.Property(e => e.Photographeddate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("photographeddate");

                entity.Property(e => e.Sampledate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("sampledate");

                entity.Property(e => e.Textileid).HasColumnName("textileid");
            });

            modelBuilder.Entity<Textilefunction>(entity =>
            {
                entity.ToTable("textilefunction");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Textilefunctionid).HasColumnName("textilefunctionid");

                entity.Property(e => e.Value)
                    .HasMaxLength(200)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<TextilefunctionTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainTextilefunctionid, e.MainTextileid })
                    .HasName("main$textilefunction_textile_pkey");

                entity.ToTable("textilefunction_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainTextilefunctionid }, "idx_main$textilefunction_textile");

                entity.Property(e => e.MainTextilefunctionid).HasColumnName("main$textilefunctionid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Textilefunction)
                    .WithMany(p => p.TextilefunctionTextiles)
                    .HasForeignKey(d => d.MainTextilefunctionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$textilefunction_textile_main$textilefunction");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.TextilefunctionsTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$textilefunction_textile_main$textile");
            });

            modelBuilder.Entity<Yarnmanipulation>(entity =>
            {
                entity.ToTable("yarnmanipulation");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Angle)
                    .HasMaxLength(20)
                    .HasColumnName("angle");

                entity.Property(e => e.Component)
                    .HasMaxLength(200)
                    .HasColumnName("component");

                entity.Property(e => e.Count)
                    .HasMaxLength(100)
                    .HasColumnName("count");

                entity.Property(e => e.Direction)
                    .HasMaxLength(20)
                    .HasColumnName("direction");

                entity.Property(e => e.Manipulation)
                    .HasMaxLength(100)
                    .HasColumnName("manipulation");

                entity.Property(e => e.Material)
                    .HasMaxLength(100)
                    .HasColumnName("material");

                entity.Property(e => e.Ply)
                    .HasMaxLength(20)
                    .HasColumnName("ply");

                entity.Property(e => e.Thickness)
                    .HasMaxLength(20)
                    .HasColumnName("thickness");

                entity.Property(e => e.Yarnmanipulationid).HasColumnName("yarnmanipulationid");
            });

            modelBuilder.Entity<YarnmanipulationTextile>(entity =>
            {
                entity.HasKey(e => new { e.MainYarnmanipulationid, e.MainTextileid })
                    .HasName("main$yarnmanipulation_textile_pkey");

                entity.ToTable("yarnmanipulation_textile");

                entity.HasIndex(e => new { e.MainTextileid, e.MainYarnmanipulationid }, "idx_main$yarnmanipulation_textile");

                entity.Property(e => e.MainYarnmanipulationid).HasColumnName("main$yarnmanipulationid");

                entity.Property(e => e.MainTextileid).HasColumnName("main$textileid");

                entity.HasOne(d => d.Yarnmanipulation)
                    .WithMany(p => p.YarnmanipulationTextiles)
                    .HasForeignKey(d => d.MainYarnmanipulationid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$yarnmanipulation_textile_main$yarnmanipulation");

                entity.HasOne(d => d.Textile)
                    .WithMany(p => p.YarnmanipulationTextiles)
                    .HasForeignKey(d => d.MainTextileid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main$yarnmanipulation_textile_main$textile");
            });

            modelBuilder.Entity<C14>(entity =>
            {
                entity.HasKey(e => e.Sample)
                    .HasName("c14_pkey");

                entity.ToTable("c14");

                entity.Property(e => e.Sample)
                    .ValueGeneratedNever()
                    .HasColumnName("sample");

                entity.Property(e => e.Agebp).HasColumnName("agebp");

                entity.Property(e => e.Area)
                    .HasMaxLength(2)
                    .HasColumnName("area");

                entity.Property(e => e.Burialnumber).HasColumnName("burialnumber");

                entity.Property(e => e.Calendardate)
                    .HasMaxLength(13)
                    .HasColumnName("calendardate");

                entity.Property(e => e.Description)
                    .HasMaxLength(12)
                    .HasColumnName("description");

                entity.Property(e => e.Eastwest)
                    .HasMaxLength(1)
                    .HasColumnName("eastwest");

                entity.Property(e => e.Northsouth)
                    .HasMaxLength(1)
                    .HasColumnName("northsouth");

                entity.Property(e => e.Squareeastwest).HasColumnName("squareeastwest");

                entity.Property(e => e.Squarenorthsouth).HasColumnName("squarenorthsouth");
            });

            modelBuilder.Entity<Detailscleaneddatum>(entity =>
            {
                entity.ToTable("detailscleaneddata");

                entity.Property(e => e.Id)
                    .HasMaxLength(17)
                    .HasColumnName("id");

                entity.Property(e => e.Adultsubadult)
                    .HasMaxLength(4)
                    .HasColumnName("adultsubadult");

                entity.Property(e => e.Ageatdeath)
                    .HasMaxLength(2)
                    .HasColumnName("ageatdeath");

                entity.Property(e => e.AnalysistypeX)
                    .HasPrecision(3, 1)
                    .HasColumnName("analysistype_x");

                entity.Property(e => e.Angle)
                    .HasMaxLength(9)
                    .HasColumnName("angle");

                entity.Property(e => e.Area)
                    .HasMaxLength(3)
                    .HasColumnName("area");

                entity.Property(e => e.Burialnumber)
                    .HasPrecision(4, 1)
                    .HasColumnName("burialnumber");

                entity.Property(e => e.BurialnumberX)
                    .HasMaxLength(2)
                    .HasColumnName("burialnumber_x");

                entity.Property(e => e.BurialnumberY)
                    .HasMaxLength(10)
                    .HasColumnName("burialnumber_y");

                entity.Property(e => e.CariesPeriodontalDisease)
                    .HasMaxLength(102)
                    .HasColumnName("caries_periodontal_disease");

                entity.Property(e => e.Clusternumber)
                    .HasMaxLength(39)
                    .HasColumnName("clusternumber");

                entity.Property(e => e.Component)
                    .HasMaxLength(9)
                    .HasColumnName("component");

                entity.Property(e => e.Count)
                    .HasPrecision(4, 1)
                    .HasColumnName("count");

                entity.Property(e => e.Dataexpertinitials)
                    .HasMaxLength(2)
                    .HasColumnName("dataexpertinitials");

                entity.Property(e => e.DateX)
                    .HasMaxLength(19)
                    .HasColumnName("date_x");

                entity.Property(e => e.Dateofexamination)
                    .HasMaxLength(25)
                    .HasColumnName("dateofexamination");

                entity.Property(e => e.Depth)
                    .HasMaxLength(5)
                    .HasColumnName("depth");

                entity.Property(e => e.Description)
                    .HasMaxLength(518)
                    .HasColumnName("description");

                entity.Property(e => e.Dimensiontype)
                    .HasMaxLength(16)
                    .HasColumnName("dimensiontype");

                entity.Property(e => e.DirectionY)
                    .HasMaxLength(7)
                    .HasColumnName("direction_y");

                entity.Property(e => e.DonebyX)
                    .HasMaxLength(8)
                    .HasColumnName("doneby_x");

                entity.Property(e => e.DorsalpittingBoolean)
                    .HasMaxLength(6)
                    .HasColumnName("dorsalpitting_boolean");

                entity.Property(e => e.Eastwest)
                    .HasMaxLength(1)
                    .HasColumnName("eastwest");

                entity.Property(e => e.Estimatestature)
                    .HasPrecision(4, 1)
                    .HasColumnName("estimatestature");

                entity.Property(e => e.Facebundles)
                    .HasMaxLength(6)
                    .HasColumnName("facebundles");

                entity.Property(e => e.Femur)
                    .HasMaxLength(10)
                    .HasColumnName("femur");

                entity.Property(e => e.Femurheaddiameter)
                    .HasPrecision(5, 2)
                    .HasColumnName("femurheaddiameter");

                entity.Property(e => e.Femurlength)
                    .HasPrecision(4, 1)
                    .HasColumnName("femurlength");

                entity.Property(e => e.Fieldbookexcavationyear)
                    .HasMaxLength(17)
                    .HasColumnName("fieldbookexcavationyear");

                entity.Property(e => e.Fieldbookpage)
                    .HasMaxLength(23)
                    .HasColumnName("fieldbookpage");

                entity.Property(e => e.Gonion)
                    .HasMaxLength(7)
                    .HasColumnName("gonion");

                entity.Property(e => e.Goods)
                    .HasMaxLength(16)
                    .HasColumnName("goods");

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(1)
                    .HasColumnName("haircolor");

                entity.Property(e => e.Headdirection)
                    .HasMaxLength(4)
                    .HasColumnName("headdirection");

                entity.Property(e => e.Humerus)
                    .HasMaxLength(10)
                    .HasColumnName("humerus");

                entity.Property(e => e.Humerusheaddiameter)
                    .HasPrecision(5, 2)
                    .HasColumnName("humerusheaddiameter");

                entity.Property(e => e.Humeruslength)
                    .HasPrecision(4, 1)
                    .HasColumnName("humeruslength");

                entity.Property(e => e.IdY)
                    .HasMaxLength(18)
                    .HasColumnName("id_y");

                entity.Property(e => e.Lamboidsuture)
                    .HasMaxLength(13)
                    .HasColumnName("lamboidsuture");

                entity.Property(e => e.Length)
                    .HasMaxLength(4)
                    .HasColumnName("length");

                entity.Property(e => e.Locale)
                    .HasMaxLength(33)
                    .HasColumnName("locale");

                entity.Property(e => e.Manipulation)
                    .HasMaxLength(4)
                    .HasColumnName("manipulation");

                entity.Property(e => e.Material)
                    .HasMaxLength(5)
                    .HasColumnName("material");

                entity.Property(e => e.MedialIpRamus)
                    .HasMaxLength(6)
                    .HasColumnName("medial_ip_ramus");

                entity.Property(e => e.Northsouth)
                    .HasMaxLength(1)
                    .HasColumnName("northsouth");

                entity.Property(e => e.Notes)
                    .HasMaxLength(859)
                    .HasColumnName("notes");

                entity.Property(e => e.Nuchalcrest)
                    .HasMaxLength(7)
                    .HasColumnName("nuchalcrest");

                entity.Property(e => e.Observations)
                    .HasMaxLength(830)
                    .HasColumnName("observations");

                entity.Property(e => e.Orbitedge)
                    .HasMaxLength(6)
                    .HasColumnName("orbitedge");

                entity.Property(e => e.Osteophytosis)
                    .HasMaxLength(6)
                    .HasColumnName("osteophytosis");

                entity.Property(e => e.Parietalbossing)
                    .HasMaxLength(25)
                    .HasColumnName("parietalbossing");

                entity.Property(e => e.Photographeddate)
                    .HasMaxLength(19)
                    .HasColumnName("photographeddate");

                entity.Property(e => e.Ply)
                    .HasMaxLength(1)
                    .HasColumnName("ply");

                entity.Property(e => e.PreauricularsulcusBoolean)
                    .HasMaxLength(1)
                    .HasColumnName("preauricularsulcus_boolean");

                entity.Property(e => e.Preservation)
                    .HasMaxLength(26)
                    .HasColumnName("preservation");

                entity.Property(e => e.Preservationindex)
                    .HasPrecision(3, 1)
                    .HasColumnName("preservationindex");

                entity.Property(e => e.Pubicbone)
                    .HasMaxLength(6)
                    .HasColumnName("pubicbone");

                entity.Property(e => e.Robust)
                    .HasMaxLength(9)
                    .HasColumnName("robust");

                entity.Property(e => e.Sampledate)
                    .HasMaxLength(19)
                    .HasColumnName("sampledate");

                entity.Property(e => e.Samplescollected)
                    .HasMaxLength(7)
                    .HasColumnName("samplescollected");

                entity.Property(e => e.Sciaticnotch)
                    .HasMaxLength(6)
                    .HasColumnName("sciaticnotch");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .HasColumnName("sex");

                entity.Property(e => e.Southtofeet)
                    .HasMaxLength(5)
                    .HasColumnName("southtofeet");

                entity.Property(e => e.Southtohead)
                    .HasMaxLength(5)
                    .HasColumnName("southtohead");

                entity.Property(e => e.Sphenooccipitalsynchrondrosis)
                    .HasMaxLength(10)
                    .HasColumnName("sphenooccipitalsynchrondrosis");

                entity.Property(e => e.Squamossuture)
                    .HasMaxLength(10)
                    .HasColumnName("squamossuture");

                entity.Property(e => e.Squareeastwest).HasColumnName("squareeastwest");

                entity.Property(e => e.Squarenorthsouth).HasColumnName("squarenorthsouth");

                entity.Property(e => e.Subpubicangle)
                    .HasMaxLength(6)
                    .HasColumnName("subpubicangle");

                entity.Property(e => e.Supraorbitalridges)
                    .HasMaxLength(6)
                    .HasColumnName("supraorbitalridges");

                entity.Property(e => e.Text)
                    .HasMaxLength(679)
                    .HasColumnName("text");

                entity.Property(e => e.Thickness)
                    .HasMaxLength(9)
                    .HasColumnName("thickness");

                entity.Property(e => e.Toothattrition)
                    .HasMaxLength(8)
                    .HasColumnName("toothattrition");

                entity.Property(e => e.Tootheruption)
                    .HasMaxLength(184)
                    .HasColumnName("tootheruption");

                entity.Property(e => e.Tootheruptionageestimate)
                    .HasMaxLength(19)
                    .HasColumnName("tootheruptionageestimate");

                entity.Property(e => e.Unnamed40)
                    .HasMaxLength(227)
                    .HasColumnName("unnamed_40");

                entity.Property(e => e.ValueX)
                    .HasMaxLength(12)
                    .HasColumnName("value_x");

                entity.Property(e => e.ValueX1)
                    .HasMaxLength(20)
                    .HasColumnName("value_x1");

                entity.Property(e => e.ValueY)
                    .HasMaxLength(36)
                    .HasColumnName("value_y");

                entity.Property(e => e.ValueY1)
                    .HasMaxLength(19)
                    .HasColumnName("value_y1");

                entity.Property(e => e.Ventralarc)
                    .HasMaxLength(3)
                    .HasColumnName("ventralarc");

                entity.Property(e => e.Westtofeet)
                    .HasMaxLength(5)
                    .HasColumnName("westtofeet");

                entity.Property(e => e.Westtohead)
                    .HasMaxLength(5)
                    .HasColumnName("westtohead");

                entity.Property(e => e.Wrapping)
                    .HasMaxLength(1)
                    .HasColumnName("wrapping");

                entity.Property(e => e.Zygomaticcrest)
                    .HasMaxLength(7)
                    .HasColumnName("zygomaticcrest");
            });
            modelBuilder.Entity<BodyanalysischartBurialmain>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bodyanalysischart_burialmain");

                entity.Property(e => e.Adultsubadult)
                    .HasMaxLength(200)
                    .HasColumnName("adultsubadult");

                entity.Property(e => e.Ageatdeath)
                    .HasMaxLength(200)
                    .HasColumnName("ageatdeath");

                entity.Property(e => e.Area)
                    .HasMaxLength(200)
                    .HasColumnName("area");

                entity.Property(e => e.BaArea)
                    .HasMaxLength(2)
                    .HasColumnName("ba_area");

                entity.Property(e => e.BaBurialnumber).HasColumnName("ba_burialnumber");

                entity.Property(e => e.BaEastwest)
                    .HasMaxLength(1)
                    .HasColumnName("ba_eastwest");

                entity.Property(e => e.BaNorthsouth)
                    .HasMaxLength(1)
                    .HasColumnName("ba_northsouth");

                entity.Property(e => e.BaSquareeastwest).HasColumnName("ba_squareeastwest");

                entity.Property(e => e.BaSquarenorthsouth).HasColumnName("ba_squarenorthsouth");

                entity.Property(e => e.Burialid).HasColumnName("burialid");

                entity.Property(e => e.Burialmaterials)
                    .HasMaxLength(5)
                    .HasColumnName("burialmaterials");

                entity.Property(e => e.Burialnumber)
                    .HasMaxLength(200)
                    .HasColumnName("burialnumber");

                entity.Property(e => e.CariesPeriodontalDisease)
                    .HasMaxLength(102)
                    .HasColumnName("caries_periodontal_disease");

                entity.Property(e => e.Clusternumber)
                    .HasMaxLength(200)
                    .HasColumnName("clusternumber");

                entity.Property(e => e.Dataexpertinitials)
                    .HasMaxLength(200)
                    .HasColumnName("dataexpertinitials");

                entity.Property(e => e.Dateofexamination)
                    .HasMaxLength(25)
                    .HasColumnName("dateofexamination");

                entity.Property(e => e.Dateofexcavation)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dateofexcavation");

                entity.Property(e => e.Depth).HasColumnName("depth");

                entity.Property(e => e.DorsalpittingBoolean)
                    .HasMaxLength(6)
                    .HasColumnName("dorsalpitting_boolean");

                entity.Property(e => e.Eastwest)
                    .HasMaxLength(200)
                    .HasColumnName("eastwest");

                entity.Property(e => e.Estimatestature)
                    .HasMaxLength(30)
                    .HasColumnName("estimatestature");

                entity.Property(e => e.Excavationrecorder)
                    .HasMaxLength(100)
                    .HasColumnName("excavationrecorder");

                entity.Property(e => e.Facebundles)
                    .HasMaxLength(200)
                    .HasColumnName("facebundles");

                entity.Property(e => e.Femur)
                    .HasMaxLength(10)
                    .HasColumnName("femur");

                entity.Property(e => e.Femurheaddiameter)
                    .HasMaxLength(5)
                    .HasColumnName("femurheaddiameter");

                entity.Property(e => e.Femurlength)
                    .HasPrecision(4, 1)
                    .HasColumnName("femurlength");

                entity.Property(e => e.Fieldbookexcavationyear)
                    .HasMaxLength(200)
                    .HasColumnName("fieldbookexcavationyear");

                entity.Property(e => e.Fieldbookpage)
                    .HasMaxLength(200)
                    .HasColumnName("fieldbookpage");

                entity.Property(e => e.Gonion)
                    .HasMaxLength(7)
                    .HasColumnName("gonion");

                entity.Property(e => e.Goods)
                    .HasMaxLength(200)
                    .HasColumnName("goods");

                entity.Property(e => e.Hair)
                    .HasMaxLength(5)
                    .HasColumnName("hair");

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(200)
                    .HasColumnName("haircolor");

                entity.Property(e => e.Headdirection)
                    .HasMaxLength(200)
                    .HasColumnName("headdirection");

                entity.Property(e => e.Humerus)
                    .HasMaxLength(10)
                    .HasColumnName("humerus");

                entity.Property(e => e.Humerusheaddiameter)
                    .HasMaxLength(5)
                    .HasColumnName("humerusheaddiameter");

                entity.Property(e => e.Humeruslength)
                    .HasPrecision(4, 1)
                    .HasColumnName("humeruslength");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lamboidsuture)
                    .HasMaxLength(17)
                    .HasColumnName("lamboidsuture");

                entity.Property(e => e.Length)
                    .HasMaxLength(200)
                    .HasColumnName("length");

                entity.Property(e => e.MedialIpRamus)
                    .HasMaxLength(6)
                    .HasColumnName("medial_ip_ramus");

                entity.Property(e => e.Northsouth)
                    .HasMaxLength(200)
                    .HasColumnName("northsouth");

                entity.Property(e => e.Notes)
                    .HasMaxLength(859)
                    .HasColumnName("notes");

                entity.Property(e => e.Nuchalcrest)
                    .HasMaxLength(7)
                    .HasColumnName("nuchalcrest");

                entity.Property(e => e.Observations)
                    .HasMaxLength(830)
                    .HasColumnName("observations");

                entity.Property(e => e.Orbitedge)
                    .HasMaxLength(6)
                    .HasColumnName("orbitedge");

                entity.Property(e => e.Osteophytosis)
                    .HasMaxLength(6)
                    .HasColumnName("osteophytosis");

                entity.Property(e => e.Parietalbossing)
                    .HasMaxLength(25)
                    .HasColumnName("parietalbossing");

                entity.Property(e => e.Photos)
                    .HasMaxLength(5)
                    .HasColumnName("photos");

                entity.Property(e => e.PreauricularsulcusBoolean)
                    .HasMaxLength(1)
                    .HasColumnName("preauricularsulcus_boolean");

                entity.Property(e => e.Preservation)
                    .HasMaxLength(200)
                    .HasColumnName("preservation");

                entity.Property(e => e.Preservationindex)
                    .HasPrecision(4, 2)
                    .HasColumnName("preservationindex");

                entity.Property(e => e.Pubicbone)
                    .HasMaxLength(6)
                    .HasColumnName("pubicbone");

                entity.Property(e => e.Robust)
                    .HasMaxLength(9)
                    .HasColumnName("robust");

                entity.Property(e => e.Samplescollected)
                    .HasMaxLength(200)
                    .HasColumnName("samplescollected");

                entity.Property(e => e.Sciaticnotch)
                    .HasMaxLength(6)
                    .HasColumnName("sciaticnotch");

                entity.Property(e => e.Sex)
                    .HasMaxLength(200)
                    .HasColumnName("sex");

                entity.Property(e => e.Shaftnumber)
                    .HasMaxLength(200)
                    .HasColumnName("shaftnumber");

                entity.Property(e => e.Southtofeet)
                    .HasMaxLength(200)
                    .HasColumnName("southtofeet");

                entity.Property(e => e.Southtohead)
                    .HasMaxLength(200)
                    .HasColumnName("southtohead");

                entity.Property(e => e.Sphenooccipitalsynchrondrosis)
                    .HasMaxLength(10)
                    .HasColumnName("sphenooccipitalsynchrondrosis");

                entity.Property(e => e.Squamossuture)
                    .HasMaxLength(10)
                    .HasColumnName("squamossuture");

                entity.Property(e => e.Squareeastwest)
                    .HasMaxLength(200)
                    .HasColumnName("squareeastwest");

                entity.Property(e => e.Squarenorthsouth)
                    .HasMaxLength(200)
                    .HasColumnName("squarenorthsouth");

                entity.Property(e => e.Subpubicangle)
                    .HasMaxLength(6)
                    .HasColumnName("subpubicangle");

                entity.Property(e => e.Supraorbitalridges)
                    .HasMaxLength(6)
                    .HasColumnName("supraorbitalridges");

                entity.Property(e => e.Text)
                    .HasMaxLength(2000)
                    .HasColumnName("text");

                entity.Property(e => e.Toothattrition)
                    .HasMaxLength(8)
                    .HasColumnName("toothattrition");

                entity.Property(e => e.Tootheruption)
                    .HasMaxLength(184)
                    .HasColumnName("tootheruption");

                entity.Property(e => e.Tootheruptionageestimate)
                    .HasMaxLength(19)
                    .HasColumnName("tootheruptionageestimate");

                entity.Property(e => e.Ventralarc)
                    .HasMaxLength(3)
                    .HasColumnName("ventralarc");

                entity.Property(e => e.Westtofeet)
                    .HasMaxLength(200)
                    .HasColumnName("westtofeet");

                entity.Property(e => e.Westtohead)
                    .HasMaxLength(200)
                    .HasColumnName("westtohead");

                entity.Property(e => e.Wrapping)
                    .HasMaxLength(200)
                    .HasColumnName("wrapping");

                entity.Property(e => e.Zygomaticcrest)
                    .HasMaxLength(7)
                    .HasColumnName("zygomaticcrest");
            });


            modelBuilder.HasSequence("excelimporter$template_nr_mxseq");

            modelBuilder.HasSequence("system$filedocument_fileid_mxseq");

            modelBuilder.HasSequence("system$queuedtask_sequence_mxseq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
