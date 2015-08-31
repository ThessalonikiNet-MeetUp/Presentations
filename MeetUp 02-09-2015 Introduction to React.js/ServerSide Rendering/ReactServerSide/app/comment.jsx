var Comment = React.createClass({
  propTypes: {
    author: React.PropTypes.object.isRequired
  },
  render() {
    return (
      <li>
        <Avatar author={this.props.author} />
        <strong>{this.props.author.name}</strong>{': '}
          {this.props.children}
      </li>
    );
  }
});