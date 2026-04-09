using Assimp;

namespace Marathon.IO
{
    public interface IAssimpSerializable
    {
        void FromAssimpScene(Scene in_scene);

        Scene ToAssimpScene();
    }
}
