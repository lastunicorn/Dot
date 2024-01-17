// Dot
// Copyright (C) 2020-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace DustInTheWind.Dot.AdventureGame.GameModel;

public class GameTimer
{
    private DateTime? startTime;
    private TimeSpan totalPlayTime;

    public TimeSpan TotalPlayTime
    {
        get => startTime.HasValue
            ? totalPlayTime + (DateTime.UtcNow - startTime.Value)
            : totalPlayTime;
        set => totalPlayTime = value;
    }

    public GameTimer()
    {
    }

    public GameTimer(TimeSpan initialValue)
    {
        totalPlayTime = initialValue;
    }

    public void Start()
    {
        if (startTime.HasValue)
            return;

        startTime = DateTime.UtcNow;
    }

    public void Stop()
    {
        if (startTime.HasValue)
        {
            totalPlayTime += DateTime.UtcNow - startTime.Value;
            startTime = null;
        }
    }

    public void Clear()
    {
        startTime = null;
        totalPlayTime = default;
    }
}