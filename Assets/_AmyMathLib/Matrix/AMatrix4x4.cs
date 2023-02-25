namespace AmyMathLib.Matrix
{
    using AmyMathLib.Vector;

    public class AMatrix4x4
    {
        public float[,] values;

        public AMatrix4x4(AVector4 column1, AVector4 column2, AVector4 column3, AVector4 column4)
        {
            values = new float[4, 4];

            // column 1
            values[0, 0] = column1.x;
            values[1, 0] = column1.y;
            values[2, 0] = column1.z;
            values[3, 0] = column1.w;

            // column 2
            values[0, 1] = column2.x;
            values[1, 1] = column2.y;
            values[2, 1] = column2.z;
            values[3, 1] = column2.w;

            // column 3
            values[0, 2] = column3.x;
            values[1, 2] = column3.y;
            values[2, 2] = column3.z;
            values[3, 2] = column3.w;

            // column 4
            values[0, 3] = column4.x;
            values[1, 3] = column4.y;
            values[2, 3] = column4.z;
            values[3, 3] = column4.w;
        }

        public AMatrix4x4(AVector3 column1, AVector3 column2, AVector3 column3, AVector3 column4)
        {
            values = new float[4, 4];

            // column 1
            values[0, 0] = column1.x;
            values[1, 0] = column1.y;
            values[2, 0] = column1.z;
            values[3, 0] = 0;

            // column 2
            values[0, 1] = column2.x;
            values[1, 1] = column2.y;
            values[2, 1] = column2.z;
            values[3, 1] = 0;

            // column 3
            values[0, 2] = column3.x;
            values[1, 2] = column3.y;
            values[2, 2] = column3.z;
            values[3, 2] = 0;

            // column 4
            values[0, 3] = column4.x;
            values[1, 3] = column4.y;
            values[2, 3] = column4.z;
            values[3, 3] = 1;
        }        

        public static AVector4 operator *(AMatrix4x4 lhs, AVector4 rhs)
        {
            AVector4 rv = new AVector4(0, 0, 0, 0);

            rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * rhs.w;
            rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * rhs.w;
            rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * rhs.w;
            rv.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * rhs.w;

            return rv;
        }
    }
}
