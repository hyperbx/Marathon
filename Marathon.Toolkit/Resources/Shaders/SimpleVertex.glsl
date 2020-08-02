#version 330 core

layout (location = 0) in vec3 _Position;
layout (location = 1) in vec3 _Normal;
layout (location = 2) in vec4 _Colour;
layout (location = 3) in vec2 _Location;

uniform mat4 _Model;
uniform mat4 _View;
uniform mat4 _Projection;
uniform vec4 _Highlight;

out vec4 _OutputColour;
out vec2 _OutputLocation;

void main()
{
	gl_Position = _Projection * _View * _Model * vec4(_Position, 1.0);

	_OutputColour = _Colour * _Highlight;
    _OutputLocation = vec2(_Location.x, _Location.y);
}