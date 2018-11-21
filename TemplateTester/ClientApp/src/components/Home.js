import React from 'react'
import { connect } from 'react-redux'

class Home extends React.Component {
	entities = [
		{ asdf: 'qwer', someNum: -5.43 }
	]

	constructor(props) {
		super(props)
	}

	render() {
		return (
			<div>
				<h1>Hello, world!</h1>
				<div>
					<h2>Entity Store</h2>
					{this.entities.length === 0 && <p>No entities stored</p>}
					<ul>
						{this.entities.map((entity, ind) => <li key={`entity-${ind}`}>{JSON.stringify(entity, null, 4)}</li>)}
					</ul>
				</div>
			</div>
		)
	}
}

export default connect()(Home)
