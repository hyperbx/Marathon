#!/usr/bin/python3 
import argparse
import csv
import re
import os.path
import sys
import struct
from collections import namedtuple

parser = argparse.ArgumentParser(description='Export collision.bin files from Sonic the Hedgehog (2006) to .obj')
parser.add_argument('files', help='collision file(s) to work with', type=argparse.FileType('r+b'), nargs="+")

Face = namedtuple('Face', ['v1', 'v2', 'v3', 'null', 'flags'])
Vertex = namedtuple('Vertex', ['x', 'y', 'z'])


def main(args):
    for file_obj in args.files:
        vertices, faces = read_collision_data(file_obj)
        
        filename = os.path.basename(os.path.splitext(file_obj.name)[0])
        groups = groups_from_flags(filename, faces)

        output_name = filename + '.obj'
        write_obj(output_name, vertices, groups)


def read_collision_data(file_obj):
    file_size, offset_table_address, table_size = struct.unpack('>III', file_obj.read(12))
    root_node_offset = 0x20
    
    file_obj.seek(root_node_offset)        
    geometry_address, mopp_code_address = struct.unpack('>II', file_obj.read(8))

    file_obj.seek(geometry_address + root_node_offset)
    vertex_section_address, face_section_address = struct.unpack('>II', file_obj.read(8))
    
    # Vertices
    file_obj.seek(vertex_section_address + root_node_offset)
    vertices_total, = struct.unpack('>I', file_obj.read(4))
    vertices = [Vertex(*struct.unpack('>fff', file_obj.read(12))) for i in range(vertices_total)]

    # Faces
    file_obj.seek(face_section_address + root_node_offset)
    faces_total, = struct.unpack('>I', file_obj.read(4))
    faces = [Face(*struct.unpack('>HHHHI', file_obj.read(12))) for i in range(faces_total)]
    
    return vertices, faces


def groups_from_flags(filename, faces):
    groups = {}
    
    for face in faces:
        group_name = '{:08x}'.format(face.flags)
        
        try:
            groups[group_name].append(face)
        except KeyError:
            groups[group_name] = [face]
            
    return groups


def write_obj(filename, vertices, groups):
    with open(filename, 'w') as output:
        output.write("mtllib collision.mtl\n")	
        
        for vertex in vertices:
            output.write('v {} {} {}\n'.format(*vertex))

        output.write("\n")
        
        output.write("usemtl collision\n")
        
        output.write("\n")
        
        for group, faces in groups.items():
            output.write('g {}\n'.format(group))
            
            for face in faces:
                face = face.v1 + 1, face.v2 + 1, face.v3 + 1
                output.write('f {} {} {}\n'.format(*face))


if __name__ == '__main__':
    args = parser.parse_args()
    main(args)