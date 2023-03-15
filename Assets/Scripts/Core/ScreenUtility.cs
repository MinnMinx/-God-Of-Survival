using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core {
    public static class ScreenUtility
    {
        public static Vector2 ScreenSizePx => new Vector2(Screen.width, Screen.height);
        public static float ScreenDiagonalSizePx => ScreenSizePx.sqrMagnitude;

        public static Vector3 BottomLeftPosition (this Camera camera)
        {
            var pos = camera.ViewportToWorldPoint(Vector3.zero);
			pos.z = 0;
			return pos;
        }

		public static Vector3 TopRightPosition(this Camera camera)
		{
			var pos = camera.ViewportToWorldPoint(Vector2.one);
			pos.z = 0;
			return pos;
		}

		public static Vector3 CenterPosition(this Camera camera)
		{
			var pos = camera.ViewportToWorldPoint(Vector2.one / 2f);
			pos.z = 0;
			return pos;
		}

		public static Vector2 ScreenSizeWorld(this Camera camera)
		{
			return TopRightPosition(camera) - BottomLeftPosition(camera);
		}

		public static float ScreenDiagonalSizeWorld(this Camera camera)
		{
			return ScreenSizeWorld(camera).sqrMagnitude;
		}

		public static Vector3 RandomPointOnScreen(this Camera camera)
		{
			var point = camera.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
			point.z = 0;
			return point;
		}

		public static Vector3 RandomPointOnEdgeScreen(this Camera camera)
		{
			bool isVertical = Random.value > 0.5f;
			bool isRightOrUpSide = Random.value > 0.5f;
			float randomVal = Random.value;
			var point = camera.ViewportToWorldPoint(
				new Vector2
				{
					x = isVertical ? isRightOrUpSide ? 1 : 0 : randomVal,
					y = isVertical ? randomVal : isRightOrUpSide ? 1 : 0,
				});
			point.z = 0;
			return point;
		}
	}
}