﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace MsbFramework.Utils
{
	public class FolderUtils
	{
		/// <summary>
		/// delete folder,
		/// </summary>
		/// <param name="path"></param>
		/// <param name="safeDelete"></param>
		/// <returns></returns>
		public static bool ClearFolder(string path)
		{
			var di = new DirectoryInfo(path);
			if (!di.Exists) return false;
			foreach (var file in di.GetFiles())
			{
				file.Delete();
			}
			foreach (var dir in di.GetDirectories())
			{
				dir.Delete(true);
			}
			return true;
		}
		public static bool CopyFilesToRootPath(string sourceRootPath, string destRootPath, SearchOption searchOption = SearchOption.AllDirectories)
		{
			string[] fileNames = Directory.GetFiles(sourceRootPath, "*", searchOption);
			foreach (string fileName in fileNames)
			{
				string destFileName = Path.Combine(destRootPath, fileName.Substring(sourceRootPath.Length));
				FileInfo destFileInfo = new FileInfo(destFileName);
				if (destFileInfo.Directory != null && !destFileInfo.Directory.Exists)
				{
					destFileInfo.Directory.Create();
				}
				File.Copy(fileName, destFileName, true);
			}
			return true;
		}
		/// <summary>
		/// 拷贝文件夹
		/// </summary>
		/// <param name="srcPath">需要被拷贝的文件夹路径</param>
		/// <param name="tarPath">拷贝目标路径</param>
		public static bool CopyFolder(string srcPath, string tarPath)
		{
			if (!Directory.Exists(srcPath))
			{
				return false;
			}
			if (!Directory.Exists(tarPath))
			{
				Directory.CreateDirectory(tarPath);
			}
			//获得源文件下所有文件
			List<string> files = new List<string>(Directory.GetFiles(srcPath));
			files.ForEach(f =>
			{
				string destFile = Path.Combine(tarPath, Path.GetFileName(f));
				File.Copy(f, destFile, true); //覆盖模式
			});

			//获得源文件下所有目录文件
			List<string> folders = new List<string>(Directory.GetDirectories(srcPath));
			folders.ForEach(f =>
			{
				string destDir = Path.Combine(tarPath, Path.GetFileName(f));
				CopyFolder(f, destDir); //递归实现子文件夹拷贝
			});
			return true;
		}
		public static bool CopyFiles(string sourceRootPath, string destRootPath, SearchOption searchOption = SearchOption.AllDirectories)
		{
			string[] fileNames = Directory.GetFiles(sourceRootPath, "*", searchOption);
			foreach (string fileName in fileNames)
			{
				FileInfo sourceFileInfo = new FileInfo(fileName);
				string destFileName = Path.Combine(destRootPath, sourceFileInfo.Name);
				FileInfo destFileInfo = new FileInfo(destFileName);
				if (destFileInfo.Directory != null && !destFileInfo.Directory.Exists)
				{
					destFileInfo.Directory.Create();
				}
				File.Copy(fileName, destFileName, true);
			}
			return true;
		}
	}
}