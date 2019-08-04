import os
import shutil
import os.path
import subprocess
import time
import json
import logging
from lib.foldercreator import make_build_directory
from lib.settings import Settings


class NugetUpdate:

    def __init__(self):
        self.source_directory = 'C:\'


