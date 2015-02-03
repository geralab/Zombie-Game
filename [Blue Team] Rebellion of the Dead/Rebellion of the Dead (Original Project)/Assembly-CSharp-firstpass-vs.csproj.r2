<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="4.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
	<Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
	<Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
	<ProductVersion>10.0.20506</ProductVersion>
	<SchemaVersion>2.0</SchemaVersion>
	<ProjectGuid>{161249EB-78E3-20B8-B069-6543AB3EE453}</ProjectGuid>
	<OutputType>Library</OutputType>
	<AppDesignerFolder>Properties</AppDesignerFolder>
	<RootNamespace></RootNamespace>
	<AssemblyName>Assembly-CSharp-firstpass</AssemblyName>
	<TargetFrameworkVersion>v3.5</TargetFrameworkVersion>
	<FileAlignment>512</FileAlignment>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
	<DebugSymbols>true</DebugSymbols>
	<DebugType>full</DebugType>
	<Optimize>false</Optimize>
	<OutputPath>Temp\bin\Debug\</OutputPath>
	<DefineConstants>DEBUG;TRACE;UNITY_STANDALONE_WIN;ENABLE_MICROPHONE;ENABLE_IMAGEEFFECTS;ENABLE_WEBCAM;ENABLE_AUDIO_FMOD;UNITY_STANDALONE;ENABLE_NETWORK;ENABLE_MONO;ENABLE_PHYSICS;ENABLE_TERRAIN;ENABLE_CACHING;ENABLE_SUBSTANCE;ENABLE_GENERICS;ENABLE_CLOTH;ENABLE_MOVIES;ENABLE_AUDIO;ENABLE_WWW;ENABLE_SHADOWS;ENABLE_DUCK_TYPING;UNITY_4_1_2;UNITY_4_1;UNITY_64;ENABLE_PROFILER;UNITY_EDITOR</DefineConstants>
	<ErrorReport>prompt</ErrorReport>
	<WarningLevel>4</WarningLevel>
	<NoWarn>0169</NoWarn>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
	<DebugType>pdbonly</DebugType>
	<Optimize>true</Optimize>
	<OutputPath>Temp\bin\Release\</OutputPath>
	<DefineConstants>TRACE</DefineConstants>
	<ErrorReport>prompt</ErrorReport>
	<WarningLevel>4</WarningLevel>
	<NoWarn>0169</NoWarn>
  </PropertyGroup>
  <ItemGroup>
	<Reference Include="System" />
    <Reference Include="System.XML" />
	<Reference Include="System.Core" />
	<Reference Include="UnityEngine">
	  <HintPath>C:/Program Files (x86)/Unity/Editor/Data/Managed/UnityEngine.dll</HintPath>
	</Reference>
	<Reference Include="UnityEditor">
	  <HintPath>C:/Program Files (x86)/Unity/Editor/Data/Managed/UnityEditor.dll</HintPath>
	</Reference>
  </ItemGroup>
  <ItemGroup>
     <Compile Include="Assets\Standard Assets\Scripts\General Scripts\ActivateTrigger.cs" />
     <Compile Include="Assets\Standard Assets\Scripts\Utility Scripts\CombineChildren.cs" />
     <Compile Include="Assets\Standard Assets\Scripts\Utility Scripts\MeshCombineUtility.cs" />
     <Compile Include="Assets\Standard Assets\Water (Basic)\Sources\Scripts\WaterSimple.cs" />
     <None Include="Assets\Standard Assets\Toon Shading\Sources\Shaders\Toony-Basic.shader" />
     <None Include="Assets\Standard Assets\Tessellation Shaders\BumpedSpecularSmooth.shader" />
     <None Include="Assets\Standard Assets\Particles\Sources\Shaders\Particle Alpha Blend (Queue +100).shader" />
     <None Include="Assets\Standard Assets\Skyboxes\_skybox info.txt" />
     <None Include="Assets\Standard Assets\Toon Shading\Sources\Shaders\Toony-BasicOutline.shader" />
     <None Include="Assets\Standard Assets\Water (Basic)\Sources\Shaders\FX-Water Simple.shader" />
     <None Include="Assets\Standard Assets\Toon Shading\Sources\Shaders\Toony-LightedOutline.shader" />
     <None Include="Assets\Standard Assets\Toon Shading\Sources\Shaders\Toony-Lighted.shader" />
     <None Include="Assets\Standard Assets\Tessellation Shaders\BumpedSpecularDisplacement.shader" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
	   Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target>
  -->
  
</Project>
