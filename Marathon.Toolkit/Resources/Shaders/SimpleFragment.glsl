#version 330 core

in vec4 Colour;
in vec2 Location;

out vec4 FragmentColour;

uniform sampler2D _Resource;

void main()
{
	vec4 @texture = texture(_Resource, Location);

	if (@texture.w < 0.1)
	{
		discard;
	}

    FragmentColour = @texture * Colour;
}