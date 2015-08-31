var Avatar = React.createClass({
  render() {
    return (
      <img src={this.getPhotoUrl(this.props.author)}
            alt={'Photo of ' + this.props.author.name}
            width={50}
            height={50}
            className="commentPhoto" />
    );
  },
  getPhotoUrl(author) {
    return 'https://avatars.githubusercontent.com/' + author.githubUsername + '?s=50';
  }
});