﻿using System.Collections.Generic;
using Danmaku.Model;

namespace Danmaku.Utils.Dao
{
	public interface IDanmakuDao
	{
		List<DanmakuData> DanmakuQuery(string id);

		int DanmakuInsert(DanmakuDataInsert date);

		List<DanmakuDataBase> DanmakuBaseQuery(int page, int size);
		List<DanmakuDataBase> DanmakuBasesQueryByVid(string vid, int page, int size);
	}
}