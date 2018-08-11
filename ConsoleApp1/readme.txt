yum install python27
yum install python-pip
pip install -U selenium

yum install Xvfb
pip install PyVirtualDisplay
env | grep DISPLAY

wget https://dl.google.com/linux/direct/google-chrome-stable_current_x86_64.rpm
sudo yum install ./google-chrome-stable_current_*.rpm
google-chrome &

cd /home/dev
wget https://chromedriver.storage.googleapis.com/2.35/chromedriver_linux64.zip
unzip chromedriver_linux64.zip